using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Orders;
using OnlineCatalog.Web.Main.Models.Products;

namespace OnlineCatalog.Web.Main.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            Products = new List<ProductViewModel>();
        }

        public IEnumerable<ProductViewModel> Products { get; set; }

        [DisplayName("Product count: ")]
        public int ProductCount
        {
            get
            {
                if (Products == null) return 0;
                return Products.Count();
            }
        }

        [DisplayName("Order state: ")]
        public OrderState OrderState { get; set; }

        [DisplayName("Last update: ")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDateTime { get; set; }

        [DisplayName("Final price: ")]
        public decimal FinalPrice
        {
            get
            {
                if (Products.IsNullOrEmpty()) return 0m;
                return Products.Sum(p => p.CalculatedPrice);
            }
        }

        public bool IsOrderButtonEnabled => !Products.IsNullOrEmpty();
    }
}