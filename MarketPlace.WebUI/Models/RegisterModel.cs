using System;
using System.ComponentModel.DataAnnotations;
 
namespace MarketPlace.WebUI.Models
{
    public class RegisterModel
    {
		[Required]
		public string Name {get;set;}
	
		[Required]
		public string Sname{get;set;}
		
		[Required]
		public string UserName{get;set;}
	
        [Required]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}