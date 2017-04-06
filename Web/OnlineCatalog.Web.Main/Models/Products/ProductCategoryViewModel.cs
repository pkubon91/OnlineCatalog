using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineCatalog.Web.Main.Models.Products
{
    public class ProductCategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid ProductCategoryGuid { get; set; }

        [DisplayName("Product category name")]
        [Required(ErrorMessage = "The name of product category is required")]
        public string ProductCategoryName { get; set; }
    }
}