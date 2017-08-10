using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineCatalog.Web.Main.Models.Products
{
    public interface IProductViewModel
    {
        ProductViewModel ProductView { get; set; }
    }

    public class AddProductViewModel : IProductViewModel
    {
        public AddProductViewModel()
        {
            ProductView = new ProductViewModel();
        }

        public AddProductViewModel(IEnumerable<ProductCategoryViewModel> shopCategories) : this()
        {
            ShopProductCategoryViewModels = shopCategories;
        }

        public ProductViewModel ProductView { get; set; }

        public IEnumerable<ProductCategoryViewModel> ShopProductCategoryViewModels { get; private set; }

        public IEnumerable<SelectListItem> ShopProductCategories
        {
            get
            {
                if(ShopProductCategoryViewModels == null) return Enumerable.Empty<SelectListItem>();
                return ShopProductCategoryViewModels.Select(c => new SelectListItem() {Text = c.ProductCategoryName, Value = c.ProductCategoryGuid.ToString()});
            }
        } 
    }
}