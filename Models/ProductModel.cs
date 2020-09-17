using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace StoreModels
{
    public class ProductModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Please Enter Price")]
        [Display(Name = "Price")]
        
        public decimal Price { set; get; }
        public bool Status { set; get; }
        //
        public CategoryModel Category { set; get; }
       [Required(ErrorMessage = "Please select Category")]
        public int CategoryID { set; get; }
    }
}
