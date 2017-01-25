using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "������� ���!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "����� ������ ������ ���� �� 3 �� 50 ��������")]
        [Display(Name = "���")]
        public string Name { get; set; }

        [Required(ErrorMessage = "������� �������!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "����� ������ ������ ���� �� 3 �� 50 ��������")]
        [Display(Name = "�������")]
        public string Sname { get; set; }

        [Required(ErrorMessage = "������� ���!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "����� ������ ������ ���� �� 3 �� 20 ��������")]
        [Display(Name = "�������")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "������� �������")]
        [Display(Name = "���.�����")]
        public string PhoneNumber { get; set; }
    }
}