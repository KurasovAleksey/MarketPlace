using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketPlace.WebUI.Models;
using Microsoft.AspNet.Identity;
using MarketPlace.WebUI.Models.AccountModels.Utils;
using Microsoft.AspNet.Identity.Owin;

namespace MarketPlace.WebUI.Controllers
{
    public class ComplaintController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Complaint
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> List(string userName)
        {
            IQueryable<Complaint> complaints;
            if (userName == null)
                complaints = db.Complaints
                    .Include(c => c.Sender)
                    .Include(c => c.Violator)
                    .Where(c => !c.isProcessed);
            else
            {
                ApplicationUser user = await UserManager.FindByNameAsync(userName);
                if (user == null) return HttpNotFound();
                complaints = db.Complaints
                    .Include(c => c.Sender)
                    .Include(c => c.Violator)
                    .Where(c => c.ViolatorId == user.Id)
                    .Where(c => c.isProcessed);
            }
            ViewBag.Violator = userName;
            return View(complaints.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult ProcessComplaint(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            complaint.isProcessed = true;
            db.Entry(complaint).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }



        //// GET: Complaint/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Complaint complaint = db.Complaints.Find(id);
        //    if (complaint == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.Entry(complaint).Reference("Sender").Load();
        //    db.Entry(complaint).Reference("Violator").Load();
        //    return View(complaint);
        //}

        //// GET: Complaint/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Complaint/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async System.Threading.Tasks.Task<ActionResult> Create(Complaint complaint)
        {
            var user = await UserManager.FindByIdAsync(complaint.ViolatorId);
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
            }
            return RedirectToAction("List", "Feedback", new { userName = user.UserName });
        }

        // GET: Complaint/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Complaint complaint = db.Complaints.Find(id);
        //    if (complaint == null)
        //    {
        //        return HttpNotFound();
        //    }
        
        //    return View(complaint);
        //}

        // POST: Complaint/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ComplaintId,Text,Datetime,isProcessed,SenderId,ViolatorId")] Complaint complaint)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(complaint).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
       
        //    return View(complaint);
        //}

        // GET: Complaint/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Complaint complaint = db.Complaints.Find(id);
        //    if (complaint == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(complaint);
        //}

        // POST: Complaint/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Complaint complaint = db.Complaints.Find(id);
        //    db.Complaints.Remove(complaint);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
