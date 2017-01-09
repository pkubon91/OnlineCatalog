using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Web.Main.Models.Shared
{
    public class AddressViewModel
    {
        public string City { get; set; }

        public string Street { get; set; }

        [Required(ErrorMessage = "Email address is required"), DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Building number")]
        public string BuildingNumber { get; set; }
    }
}