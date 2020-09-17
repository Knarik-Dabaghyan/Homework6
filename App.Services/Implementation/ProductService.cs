using App.Services.Interfaces;
using Services.Interfaces;
using StoreModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Services.Implementation
{
    //dll-ic durs hasaneli chlni
    internal class ProductService:IProductService
    {
        public const string connectionstring = "Data Source=LocalHost;Initial Catalog=Example;Integrated Security=True";

        public void Add(ProductModel model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string cmd = string.Format("INSERT INTO Product(Name, Price,CategoryId) VALUES('{0}', {1},{2})", model.Name, model.Price,model.CategoryID);
                SqlCommand command = new SqlCommand(cmd, sqlConnection);
                //command.CommandText = "INSERT INTO Products (Name, Price) VALUES('Pen', 10)";
                //command.Connection = sqlConnection;
                int res = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string sqlcommand = $"update Product set Status = 0 where ID={id}";
                SqlCommand command = new SqlCommand(sqlcommand, sqlConnection);
                command.ExecuteNonQuery();
            }
        }
        public void DeletewithCategory(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string sqlcommand = $"update Product set Status = 0 where CategoryId={id}";
                SqlCommand command = new SqlCommand(sqlcommand, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ProductModel model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                string sqlcommand = $"update Product set Name='{model.Name}',Price={model.Price},CategoryId={model.CategoryID} where ID={model.ID}";
                SqlCommand command = new SqlCommand(sqlcommand, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public async Task<ProductModel> Get(int id)
        {
            ProductModel product = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"select ID,Name,Price,Status,CategoryId from Product where Status=1 and ID={id}";
                command.Connection = sqlConnection;
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        if (await dataReader.ReadAsync())
                        {
                             product = new ProductModel()
                            {
                                ID = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Price = dataReader.GetDecimal(2),
                                Status = dataReader.GetBoolean(3),
                            };
                           
                        }
                    }
                }
                return product;

            }
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "select ID,Name,Price,Status,CategoryID from Product where Status=1";
                command.Connection = sqlConnection;
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            ProductModel product = new ProductModel()
                            {
                                ID = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Price = dataReader.GetDecimal(2),
                                Status = dataReader.GetBoolean(3),
                                CategoryID=dataReader.GetInt32(4),
                            };
                            products.Add(product);
                            #region porc
                            //Type t = dataReader["CategoryID"].GetType();
                            //if (DBNull.Value.Equals(dataReader["CategoryID"]))
                            //{
                            //    product.CategoryID = null;
                            //    products.Add(product);
                            //    continue;
                            //}
                            //else
                            //{
                            //    product.CategoryID = (int)dataReader["CategoryID"];
                            //    ICategoryService category = new CategoryService();
                            //    product.Category = await category.Get((int)product.CategoryID);
                            //    products.Add(product);                  
                            //}
                            #endregion
                        }
                    }
                }

            }
            return products;
        }
    }
}
