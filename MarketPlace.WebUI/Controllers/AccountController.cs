using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MarketPlace.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using MarketPlace.WebUI.Models.AccountModels.Utils;
using MarketPlace.WebUI.Models.ViewModels;

namespace MarketPlace.WebUI.Controllers
{
    public class AccountController : Controller
    {
        #region Register

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Name = model.Name, Sname = model.Sname, UserName = model.UserName, 
				Email = model.Email, RegistrationDate = DateTime.Now, PhoneNumber = model.PhoneNumber};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Manage", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        #endregion

        #region Login - Logout

        private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}
 
		public ActionResult Login(string returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}
 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
				if (user == null)
				{
					ModelState.AddModelError("", "Неверный логин или пароль.");
				}
				else
				{
                    await SignInAsync(user, isPersistent: false);
                    if (String.IsNullOrEmpty(returnUrl))
						return RedirectToAction("Index", "Home");
					return Redirect(returnUrl);
				}
			}
			ViewBag.returnUrl = returnUrl;
			return View(model);
		}
		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Delete user

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region User account

        public async Task<ActionResult> Manage()
        {
            ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }

        //public ActionResult LoginPartial()
        //{
        //    return PartialView();
        //}

        #endregion

        #region Edit user

        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditViewModel model = new EditViewModel { PhoneNumber = user.PhoneNumber };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.PhoneNumber = model.PhoneNumber;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        #endregion

        #region Common methods

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            var identity = await UserManager.CreateIdentityAsync(
               user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(
               new AuthenticationProperties()
               {
                   IsPersistent = isPersistent
               }, identity);
        }

        #endregion
    }
}