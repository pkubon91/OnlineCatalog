using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OnlineCatalog.Web.Main.Models.Shared;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.Models.Groups
{
    public class ShopViewModel : IAddressViewModel
    {
        public ShopViewModel()
        {
            Address = new AddressViewModel();
        }

        [HiddenInput(DisplayValue = false)]
        public Guid ShopGuid { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsActive { get; set; }

        [DisplayName("Shop name")]
        [Required(ErrorMessage = "Shop name field is required")]
        public string ShopName { get; set; }

        public AddressViewModel Address { get; set; }
    }
}