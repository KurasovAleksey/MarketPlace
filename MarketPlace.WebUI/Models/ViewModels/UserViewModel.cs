using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Sname { get; set; }

        [Display(Name = "Ник")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Бан")]
        public bool IsBanned { get; set; }

        [Display(Name = "Уровень")]
        public string TopRole { get; set; }

        public string RoleId { get; set; }
    }
}