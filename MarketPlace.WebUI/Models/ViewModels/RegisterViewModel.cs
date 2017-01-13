using System;
using System.ComponentModel.DataAnnotations;
 
namespace MarketPlace.WebUI.Models.ViewModels
{
    public class RegisterViewModel
    {
		[Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Имя")]
        public string Name {get;set;}
	
		[Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string Sname{get;set;}
		
		[Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [Display(Name = "Никнейм")]
        public string UserName{get;set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Моб.номер")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string PasswordConfirm { get; set; }
    }
}