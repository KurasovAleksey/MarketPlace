using Microsoft.Owin;
using Owin;
using MarketPlace.WebUI.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
 
[assembly: OwinStartup(typeof(MarketPlace.WebUI.Startup))]
 
namespace MarketPlace.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ����������� �������� � ��������
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}