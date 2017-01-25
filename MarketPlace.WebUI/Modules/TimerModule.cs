using MarketPlace.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MarketPlace.WebUI.Modules
{
    public class TimerModule : IHttpModule
    {
        static Timer timer;
        long interval = 60000;
        static object synclock = new object();
        ApplicationDbContext db = new ApplicationDbContext();

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(ChooseWinners), null, 0, interval);
        }

        public void ChooseWinners(object obj)
        {
            lock (synclock)
            {
                using (db = new ApplicationDbContext())
                {
                    db.Database.SqlQuery<int>("exec ChooseWinners");
                }
            }
        }

        public void Dispose()
        {

        }


    }
}