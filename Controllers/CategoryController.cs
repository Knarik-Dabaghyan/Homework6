using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using StoreModels;

namespace Ado_Net_Ex_1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService = null;
        private readonly IProductService _productservice = null;

        public CategoryController(ICategoryService categoryService, IProductService productservice)
        {
            _categoryService = categoryService;
            _productservice = productservice;
        }
        // GET: CategoryController
        public /*async*/ /*Task*/ActionResult Index()
        {
            return View(/*await*/ _categoryService.GetCategories());
        }

        // GET: CategoryController/Details/5
        public async  Task<ActionResult> Details(int id)
        {
           CategoryModel category=await _categoryService.Get(id);
            return View(category);
        }

        // GET: CategoryController/Create
        [HttpPost]
        public ActionResult Create([Bind("Name")] CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(model);
                return RedirectToAction("Index");
            }
            return View("Index");
                
           
            
        }

        // POST: CategoryController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
             CategoryModel model= await _categoryService.Get(id);
            return View(model);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Edit(model);
                return RedirectToAction("Index");
            }
            return View("Index");
           
        }

        // GET: CategoryController/Delete/
        public ActionResult Delete(int id)
        {
            _categoryService.Delete(id);
           // _productservice.DeletewithCategory(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
