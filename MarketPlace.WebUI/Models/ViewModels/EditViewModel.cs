using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Введите имя!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string Sname { get; set; }

        [Required(ErrorMessage = "Введите ник!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [Display(Name = "Никнейм")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите телефон")]
        [Display(Name = "Моб.номер")]
        public string PhoneNumber { get; set; }
    }
}