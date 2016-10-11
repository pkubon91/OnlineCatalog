using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Web.Main.Models.UserModel
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required, DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("Repeat password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Email address is required"), DisplayName("Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}