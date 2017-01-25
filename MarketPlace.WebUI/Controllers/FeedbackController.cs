using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketPlace.WebUI.Models;
using MarketPlace.WebUI.Models.AccountModels.Utils;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace MarketPlace.WebUI.Controllers
{
    
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Feedback
        [Authorize(Roles = "User")]
        public async Task<ActionResult> List(string userName)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(userName);
            if(user == null) return HttpNotFound();
            var feedbacks = db.Feedbacks
                .Include(f => f.FeedbackSender)
                .Include(f => f.FeedbackReceiver)
                .Where(f => f.FeedbackReceiver.UserName == userName)
                .OrderByDescending(f => f.Datetime);
            ViewBag.Receiver = user.UserName;
            ViewBag.ReceiverId = user.Id;
            return View(feedbacks.ToList());
        }

        // GET: Feedback/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    if (feedback == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(feedback);
        //}

        // GET: Feedback/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Feedback/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Create(Feedback feedback)
        {
            var user = await UserManager.FindByIdAsync(feedback.FeedbackReceiverId);
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
            }
            return RedirectToAction("List", new { userName = user.UserName });
        }

        //// GET: Feedback/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    if (feedback == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(feedback);
        //}

        //// POST: Feedback/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FeedbackId,Comment,Datetime,FeedbackSenderId,FeedbackReceiverId")] Feedback feedback)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(feedback).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("List");
        //    }

        //    return View(feedback);
        //}

        // GET: Feedback/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    int creatorId = User.Identity.Name
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    if (feedback == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if(feedback.)
        //    return View(feedback);
        //}

        // POST: Feedback/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            var user = await UserManager.FindByIdAsync(feedback.FeedbackReceiverId);
            return RedirectToAction("List", new { userName = user.UserName });
        }

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
