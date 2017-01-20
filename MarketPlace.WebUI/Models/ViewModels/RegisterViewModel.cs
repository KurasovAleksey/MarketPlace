using System;
using System.ComponentModel.DataAnnotations;
 
namespace MarketPlace.WebUI.Models.ViewModels
{
    public class RegisterViewModel
    {
		[Required(ErrorMessage = "Введите имя!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Имя")]
        public string Name {get;set;}

        [Required(ErrorMessage = "Введите фамилию!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string Sname{get;set;}

        [Required(ErrorMessage = "Введите ник!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [Display(Name = "Никнейм")]
        public string UserName{get;set;}

        [Required(ErrorMessage = "Введите Email!")]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите телефон")]
        [Display(Name = "Моб.номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string PasswordConfirm { get; set; }
    }
}