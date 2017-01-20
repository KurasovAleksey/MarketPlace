using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
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
using System.Net;
using System.Data.Entity;

namespace MarketPlace.WebUI.Controllers
{
    public class AccountController : Controller
    {
        static int paginationDiv = 10;
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Register


        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IEnumerable<ApplicationRole> Roles
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.AsEnumerable();
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
				Email = model.Email, PhoneNumber = model.PhoneNumber};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Users", "Account", new { userName = user.UserName });
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
                string userName = (await UserManager.FindByEmailAsync(model.Email)).UserName;
                ApplicationUser user = await UserManager.FindAsync(userName, model.Password);
				if (user == null)
				{
					ModelState.AddModelError("", "Неверный логин или пароль.");
				}
                else if (user.isBanned)
                {
                    ModelState.AddModelError("", "Вы были заблокированы по решению администрации.");
                }
				else
				{
                    await SignInAsync(user, isPersistent: false);
                    if (String.IsNullOrEmpty(returnUrl))
						return RedirectToAction("Users", "Account", new { userName = userName });
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

        public async Task<ActionResult> Users(string userName)
        {
            if(userName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ApplicationUser user = await UserManager.FindByNameAsync(userName);
            if (user == null)
                return HttpNotFound();
            
            int topRoleId = user.Roles.LastOrDefault().RoleId;
            ViewBag.Role = (from role in Roles where role.Id == topRoleId select role.Name).FirstOrDefault();
            ViewBag.RegDate = String.Format("{0:d}", user.Roles.FirstOrDefault().StartDate);
            return View(user);
        }

        #endregion

        #region UserPaging

        [HttpGet]
        public async Task<ActionResult> List(string userName = "")
        {

            IQueryable<UserViewModel> usersList = null;

            {
                usersList = (from user in db.Users
                             where user.Name.Contains(userName) || user.Sname.Contains(userName)
                             select new UserViewModel()
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Sname = user.Sname,
                                 UserName = user.UserName,
                                 PhoneNumber = user.PhoneNumber,
                                 Email = user.Email,
                                 IsBanned = user.isBanned
                             });
            }

            ViewBag.CurrentPage = 1;
            ViewBag.LastPage = Math.Ceiling(Convert.ToDouble(usersList.ToList().Count) / paginationDiv);

            var userManager = UserManager;
            var usersListAc = await usersList.Take(paginationDiv).ToListAsync();
            foreach (var user in usersListAc)
            {
                bool r = userManager.IsInRole(user.Id, "Admin");
                user.TopRole = r ? "Admin" : "User";
            }

            userManager.Dispose();
            return View(usersListAc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> List(int CurrentPage, int LastPage, string userName = "")
        {
            var usersList = (from user in db.Users
                             where user.Name.Contains(userName) || user.Sname.Contains(userName)
                             select new UserViewModel()
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Sname = user.Sname,
                                 UserName = user.UserName,
                                 PhoneNumber = user.PhoneNumber,
                                 Email = user.Email,
                                 IsBanned = user.isBanned
                             }).OrderBy(u => u.Id)
                             .Skip((CurrentPage - 1) * paginationDiv)
                             .Take(paginationDiv);

            ViewBag.CurrentPage = CurrentPage;

            var userManager = UserManager;
            var usersListAc = await usersList.ToListAsync();
            foreach (var user in usersListAc)
            {
                bool r = userManager.IsInRole(user.Id, "Admin");
                user.TopRole = r ? "Admin" : "User";
            }

            userManager.Dispose();
            return PartialView("_ListUsers", usersListAc);
        }

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

        //Edit permissions

        public ActionResult EditUserRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int intId = id.Value;
            ApplicationUser user = UserManager.FindById(intId);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (UserManager.IsInRole(intId, "Admin")) UserManager.RemoveFromRole(intId, "Admin");
            else UserManager.AddToRoles(intId, "Admin");
            IdentityResult result = UserManager.Update(user);
            if (!result.Succeeded)
                ModelState.AddModelError("", "Что-то пошло не так");
            return View("List");
        }

        public ActionResult EditUserBan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.isBanned = !user.isBanned;
            if(user.isBanned)
            {
                UserManager.RemoveFromRole(user.Id, "User");
                if (UserManager.IsInRole(user.Id, "Admin"))
                    UserManager.RemoveFromRole(user.Id, "Admin");
            }
            else {
                if (!UserManager.IsInRole(user.Id, "User"))
                    UserManager.AddToRole(user.Id, "User");
            }
            IdentityResult result = UserManager.Update(user);
            if (!result.Succeeded)
                ModelState.AddModelError("", "Что-то пошло не так");
            return View("List");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}