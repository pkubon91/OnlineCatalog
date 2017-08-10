using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OnlineCatalog.Web.Main.CustomValidations;

namespace OnlineCatalog.Web.Main.Models.Products
{
    public class ProductViewModel
    {
        private decimal _defaultPrice;

        [HiddenInput(DisplayValue = false)]
        public string CreatedLogin { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid ShopGuid { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid ProductGuid { get; set; }

        [DisplayName("Product name")]
        public string ProductName { get; set; }
        
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Default price")]
        [DataType(DataType.Currency, ErrorMessage = "Currency format is not valid")]
        public decimal DefaultPrice
        {
            get { return Math.Round(_defaultPrice, 2); }
            set { _defaultPrice = value; }
        }

        [DisplayName("Active")]
        public bool IsActive { get; set; } = true;
        
        [DisplayName("Tax percent")]
        [IntegerValidator(minValue: 0, maxValue: 100)]
        public int TaxPercent { get; set; }

        [DisplayName("Price after tax")]
        public decimal CalculatedPrice
        {
            get { return Math.Round(DefaultPrice + TaxPrice, 2); }
        }

        public decimal TaxPrice
        {
            get { return DefaultPrice*TaxPercent*0.01m; }
        }

        [DisplayName("Choose categories")]
        public IEnumerable<string> AssignedCategories { get; set; } 
    }
}