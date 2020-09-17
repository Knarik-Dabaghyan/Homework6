using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace StoreModels
{
    public class CategoryModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }

        
        [Display(Name = "Name")]
        [Required(ErrorMessage ="Please enter Name ")]
        public string Name { set; get; }
      
        public bool Status { set; get; }
       
    }
}
