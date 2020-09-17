using App.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddService(this IServiceCollection service)
        {
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<ICategoryService, CategoryService>();
        }
    }
}
