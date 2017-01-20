using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class AuctionViewModel
    {
        public int AuctionID { get; set; }

        [Required(ErrorMessage = "Введите название лота")]
        [Display(Name = "Название")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 1000 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание лота")]
        [Display(Name = "Описание лота")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 1000 символов")]
        public string Description { get; set; }

        [Display(Name = "Дополнительная информация")]
        [MaxLength(400)]
        public string Information { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        [Range(0.01, 1000000000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Валюта")]
        public string Currency { get; set; }

        //[MaxLength(1000)]

        //public string PicturePath { get; set; }

        [Display(Name = "Категория лота")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Недопустимая длительность")]
        [Display(Name = "Длительность(дни, от 1 до 10)")]
        public int DaysDuration { get; set; }
    }



    public enum Currency
    {
        UAH,
        USD
    }
}