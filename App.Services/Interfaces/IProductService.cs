using StoreModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace App.Services.Interfaces
{
   public  interface IProductService
    {
         Task<IEnumerable<ProductModel>> GetProducts();
         Task<ProductModel> Get(int id);
         void Add(ProductModel model);
         void Edit(ProductModel model);
         void Delete(int id);
        void DeletewithCategory(int id);
    }
}
