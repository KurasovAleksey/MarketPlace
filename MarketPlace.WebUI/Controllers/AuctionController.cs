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
    public class AuctionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Auction
        public async Task<ActionResult> All()
        {
            var auctions = db.Auctions.Include(a => a.Category).Include(a => a.User);
            return View(await auctions.ToListAsync());
        }

        // GET: Auction/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = await db.Auctions.FindAsync(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }

        // GET: Auction/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title");
            return View();
        }

        // POST: Auction/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuctionId,Title,Description,Price,Information,CreationDate,PicturePath,UserId,CategoryId,FinishDate")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                db.Auctions.Add(auction);
                await db.SaveChangesAsync();
                return RedirectToAction("All");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auction);
        }

        // GET: Auction/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = await db.Auctions.FindAsync(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auction);
        }

        // POST: Auction/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuctionId,Title,Description,Price,Information,CreationDate,PicturePath,UserId,CategoryId,FinishDate")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("All");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auction);
        }

        // GET: Auction/Delete/5
        public async Task<ActionResult> Finish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = await db.Auctions.FindAsync(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }

        // POST: Auction/Delete/5
        [HttpPost, ActionName("Finish")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FinishConfirmed(int id)
        {
            Auction auction = await db.Auctions.FindAsync(id);
            auction.FinishDate = DateTime.Now;
            db.Entry(auction).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("All");
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
