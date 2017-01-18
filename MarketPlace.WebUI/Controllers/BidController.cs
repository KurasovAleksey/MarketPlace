using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketPlace.WebUI.Models;

namespace MarketPlace.WebUI.Controllers
{
    public class BidController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bid
        public async Task<ActionResult> Index()
        {
            var bids = db.Bids.Include(b => b.Auction).Include(b => b.User);
            return View(await bids.ToListAsync());
        }

        //// GET: Bid/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bid bid = await db.Bids.FindAsync(id);
        //    if (bid == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bid);
        //}

        // GET: Bid/Create
        public ActionResult Create()
        {
            ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "Title");
            return View();
        }

        // POST: Bid/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "Title", bid.AuctionId);
            return View(bid);
        }

        //// GET: Bid/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bid bid = await db.Bids.FindAsync(id);
        //    if (bid == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "Title", bid.AuctionId);
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Name", bid.UserId);
        //    return View(bid);
        //}

        //// POST: Bid/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "BidId,Amount,Datetime,IsFinalBid,UserId,AuctionId")] Bid bid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bid).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "Title", bid.AuctionId);
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Name", bid.UserId);
        //    return View(bid);
        //}

        // GET: Bid/Delete/5
        public async Task<ActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = await db.Bids.FindAsync(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bid/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelConfirmed(int id)
        {
            Bid bid = await db.Bids.FindAsync(id);
            db.Bids.Remove(bid);
            await db.SaveChangesAsync();
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
