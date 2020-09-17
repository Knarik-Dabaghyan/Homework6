using StoreModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Services.Interfaces
{
   public  interface ICategoryService
    {
       IEnumerable<CategoryModel> GetCategories();
        Task<CategoryModel> Get(int id);
        void Add(CategoryModel model);
        void Edit(CategoryModel model);
        void Delete(int id);
    }
}
