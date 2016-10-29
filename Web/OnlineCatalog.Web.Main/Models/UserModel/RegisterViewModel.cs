using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnlineCatalog.Web.Main.CustomValidations;

namespace OnlineCatalog.Web.Main.Models.UserModel
{
    [ValidateUser]
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Address = new AddressViewModel();
            ShopNames = new Dictionary<string, bool>();
        }

        [Required]
        [DisplayName("User name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required, DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("Repeat password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        public AddressViewModel Address { get; set; }

        public Dictionary<string, bool> ShopNames { get; set; }
    }
}