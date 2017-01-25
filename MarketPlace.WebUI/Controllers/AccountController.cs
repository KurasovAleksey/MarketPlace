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

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    Sname = model.Sname,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
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
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Users(string userName)
        {
            if (userName == null)
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
        [Authorize(Roles = "Admin")]
        public ActionResult List(string search, bool? admins = false, bool? banned = false, int page = 1)
        {
            int pageSize = 1;

            ViewBag.Search = search;
            ViewBag.Admins = admins;
            ViewBag.Banned = banned;

            IEnumerable<ApplicationUser> users = db.Users.Include(u => u.Roles);

            if (admins != null && admins != false)
            {
                var roleList = db.Roles.Where(r => r.Name == "Admin");
                if(roleList.Any())
                {
                    var roleId = roleList.First().Id;
                    users = users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
                }
            }

            if (banned != null && banned != false)
                users = users.Where(u => u.isBanned);

            if (!String.IsNullOrEmpty(search))
                users = users
                    .Where(u => (u.Name + " " + u.Sname).Contains(search) 
                    || u.UserName.Contains(search));
            
            IEnumerable<ApplicationUser> usersPerPage = users.Skip((page - 1) * pageSize).Take(pageSize);

            List<UserViewModel> userViews = (from user in usersPerPage
                                             select new UserViewModel()
                                             {
                                                 Id = user.Id,
                                                 Name = user.Name,
                                                 Sname = user.Sname,
                                                 UserName = user.UserName,
                                                 PhoneNumber = user.PhoneNumber,
                                                 Email = user.Email,
                                                 IsBanned = user.isBanned
                                             }).ToList();

            var userManager = UserManager;
            foreach (var user in userViews)
            {
                bool r = userManager.IsInRole(user.Id, "Admin");
                user.TopRole = r ? "Админ" : "Пользователь";
            }
            userManager.Dispose();

            PageInfo pageInfo = new PageInfo
            { PageNumber = page, PageSize = pageSize, TotalItems = users.ToList().Count };

            UsersListViewModel ulvm = new UsersListViewModel()
            {
                Users = userViews,
                PageInfo = pageInfo
            };
            
            return View(ulvm);
        }


        #endregion

        #region Edit user
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult EditUserRole(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindByName(userName);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (UserManager.IsInRole(user.Id, "Admin")) UserManager.RemoveFromRole(user.Id, "Admin");
            else UserManager.AddToRoles(user.Id, "Admin");
            IdentityResult result = UserManager.Update(user);
            if (!result.Succeeded)
                ModelState.AddModelError("", "Что-то пошло не так");
            return View("List");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUserBan(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindByName(userName);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.isBanned = !user.isBanned;
            if (user.isBanned)
            {
                UserManager.RemoveFromRole(user.Id, "User");
                if (UserManager.IsInRole(user.Id, "Admin"))
                    UserManager.RemoveFromRole(user.Id, "Admin");
            }
            else
            {
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