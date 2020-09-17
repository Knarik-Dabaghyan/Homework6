using Services.Interfaces;
using StoreModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    class CategoryService : ICategoryService
    {
        public const string connectionstring = "Data Source=LocalHost;Initial Catalog=Example;Integrated Security=True";
        public void Add(CategoryModel model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string cmd = string.Format("INSERT INTO Category(Name) VALUES('{0}')", model.Name);
                SqlCommand command = new SqlCommand(cmd, sqlConnection);
                //command.CommandText = "INSERT INTO Products (Name, Price) VALUES('Pen', 10)";
                //command.Connection = sqlConnection;
                int res = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            int result_1=-1, result_2 = -1;
            string cmdForCategory = $"update Category set Status = 0 where ID={id}";
            string cmdForProduct = $"update Product set Status = 0 where CategoryId={id}";
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(cmdForCategory, sqlConnection, transaction))
                    {
                        result_1 = command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(cmdForProduct, sqlConnection, transaction))
                    {
                        result_2 = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                        
                }
            }
            catch 
            {

                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Edit(CategoryModel model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string sqlcommand = $"update Category set Name='{model.Name}' where ID={model.ID}";
                SqlCommand command = new SqlCommand(sqlcommand, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public  async Task<CategoryModel> Get(int id)
        {
            CategoryModel category = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"select ID,Name,Status from Category where Status=1 and ID={id}";
                command.Connection = sqlConnection;
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        if (await dataReader.ReadAsync())
                        {
                            category = new CategoryModel()
                            {
                                ID = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Status = dataReader.GetBoolean(2),
                            };
                        }
                    }
                }
                return category;
            }
        }

        public  /*async*/ /*Task*/IEnumerable<CategoryModel> GetCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "select ID,Name,Status from Category where Status=1";
                command.Connection = sqlConnection;
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (/*await*/ dataReader.Read/*Async*/())
                        {
                            CategoryModel category = new CategoryModel()
                            {
                                ID = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Status = dataReader.GetBoolean(2),
                            };
                            categories.Add(category);
                        }
                    }
                }

            }
            return categories;
        }
    }
}
