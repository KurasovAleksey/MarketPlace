using System.ComponentModel.DataAnnotations;

namespace MarketPlace.WebUI.Models
{

    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}