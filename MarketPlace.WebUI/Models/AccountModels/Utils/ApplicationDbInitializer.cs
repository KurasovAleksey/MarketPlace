using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.AccountModels.Utils
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }
   
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string roleNameAdmin = "Администратор";
            const string descriptionAdmin = "Управление полномочиями, модерация, рассмотр жалоб";
            const string roleNameUser = "Непривилегированный пользователь";
            const string descriptionUser = "Базовый уровень полномочий: право участия в торгах";


            var role = roleManager.FindByName(roleNameAdmin);
            if (role == null)
            {
                role = new ApplicationRole(roleNameAdmin) { Description = descriptionAdmin };
                var roleresult = roleManager.Create(role);
            }
            role = roleManager.FindByName(roleNameUser);
            if (role == null)
            {
                role = new ApplicationRole(roleNameUser) { Description = descriptionUser };
                var roleresult = roleManager.Create(role);
            }
        }
    }
}