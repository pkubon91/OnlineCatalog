using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Web.Main.Models.UserModel
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}