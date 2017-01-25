using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}