using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketPlace.WebUI.Models;
using System.Data.Entity.Infrastructure;

namespace MarketPlace.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class BidController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bid
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.Auction).Include(b => b.User);
            return View(bids.ToList());
        }

        public ActionResult List(int? id)
        {
            var bids = db.Bids.Include(b => b.User);
            bids = bids.Where(b => b.AuctionId == id.Value);
            return View(bids.ToList());
        }

        // GET: Bid/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // GET: Bid/Create
        public ActionResult Create()
        {
            ViewBag.BidError = "";
            return View();
        }

        // POST: Bid/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidId,Amount,Datetime,IsFinalBid,UserId,AuctionId")] Bid bid)
        {
            bid.Time = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                try
                {
                    db.SaveChanges();
                } catch(DbUpdateException)
                {
                    db.Bids.Remove(bid);
                    ViewBag.AuctionId = bid.AuctionId;
                    return View("BidCreatingError");
                }
                
            }
            return RedirectToAction("Details", "Auction", new { id = bid.AuctionId });
        }

        public ViewResult BidCreatingError(int auctionId)
        {
            ViewBag.AuctionId = auctionId;
            return View();
        }

        // GET: Bid/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bid/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BidId,Amount,Datetime,IsFinalBid,UserId,AuctionId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bid);
        }

        // GET: Bid/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bid bid = db.Bids.Find(id);
            db.Bids.Remove(bid);
            db.SaveChanges();
            return RedirectToAction("Index");
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
