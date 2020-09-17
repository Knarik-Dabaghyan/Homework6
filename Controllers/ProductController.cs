using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Ado_Net_Ex_1.Models;

using App.Services.Interfaces;
using StoreModels;
using System.Data;
using Services.Interfaces;

namespace Ado_Net_Ex_1.Controllers
{
    public class ProductController : Controller
    {
       
        private readonly IProductService _productservice;
        private readonly ICategoryService _categoryService;

        //erb startup.cs-um new a linum avtomat poxancuma es ctor-in
        public ProductController(IProductService service,ICategoryService categoryService)
        {
            _productservice = service;
            _categoryService = categoryService;
            
        }

        // GET: Product
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            ViewData["CategoryList"] = _categoryService.GetCategories();
            IEnumerable<ProductModel> products = await _productservice.GetProducts();
            foreach (var model in products)
            {
                CategoryModel category = await _categoryService.Get((int)model.CategoryID);
                model.Category = category;
            }
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = await _productservice.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public /*async*/ /*Task*/IActionResult Create(/*[Bind("ID,Name,Price,CategoryId")]*/ ProductModel model)
        {
           
                //ViewData["CategoryList"] = _categoryService.GetCategories();
                //_productservice.Add(model);
                //return RedirectToAction("Index");

            // ViewData["CategoryList"] =await _categoryService.GetCategories();
            //     ViewBag.CategoryNames =  _categoryService.GetCategories();
            if (ModelState.IsValid)
            {
                _productservice.Add(model);
            return RedirectToAction("Index");
            }
            return View("Index");

        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Name,Price")] ProductModel product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_context.Add(product);
        //        //await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            ViewData["CategoryList"] = _categoryService.GetCategories();
            var product = await _productservice.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit( /*[Bind("ID,Name,Price")]*/ ProductModel product)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _productservice.Edit(product);
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public  IActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            _productservice.Delete(id);
            return RedirectToAction(nameof(Index));
           
        }

        // POST: Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var product = await _context.Product.FindAsync(id);
        //    //_context.Product.Remove(product);
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductExists(int id)
        {
            return true;
            //return _context.Product.Any(e => e.ID == id);
        }
    }
}
