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
using MarketPlace.WebUI.Models.ViewModels;
using PagedList;

namespace MarketPlace.WebUI.Controllers
{
    public class AuctionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Users Auctions
        // GET: Auction
        public ActionResult List(string user, int? category, string search, int page = 1)
        {
            int pageSize = 10;

            ViewBag.User = user;
            ViewBag.Search = search;
            ViewBag.Category = category;

            IEnumerable<Auction> auctions = db.Auctions
                .Include(a => a.User)
                .Include(a => a.Category);

            IEnumerable<Auction> auctionsPerPage = null;

            if (category != null && category != 0)
                auctionsPerPage = auctions.Where(a => a.CategoryId == category)
                    .Skip((page - 1) * pageSize).Take(pageSize);
            if (!String.IsNullOrEmpty(user))
                auctionsPerPage = auctions.Where(a => a.User.UserName == user)
                    .Skip((page - 1) * pageSize).Take(pageSize);
            if (!String.IsNullOrEmpty(search))
                auctionsPerPage = auctions.Where(a => a.Title.Contains(search))
                    .Skip((page - 1) * pageSize).Take(pageSize);

            List<Category> categories = db.Categories.ToList();
            categories.Insert(0, new Category { Title = "Все", CategoryId = 0 });

            PageInfo pageInfo = new PageInfo
            { PageNumber = page, PageSize = pageSize, TotalItems = auctions.ToList().Count };

            AuctionListViewModel alvm = new AuctionListViewModel()
            {
                Auctions = auctionsPerPage,
                Categories = new SelectList(categories, "CategoryId", "Title"),
                PageInfo = pageInfo
            };

            
            return View(alvm);
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
            db.Entry(auction).Reference("User").Load();
            db.Entry(auction).Collection("Bids").Load();
            db.Entry(auction).Reference("Category").Load();
            ViewBag.Currency = (auction.IsNationalCurrency) 
                ? Currency.UAH.ToString() 
                : Currency.USD.ToString();
            ViewBag.AuctionId = id;
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
        public async Task<ActionResult> Create(AuctionCreateModel auction)
        {
           
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                int id = db.Users.Where(u => u.UserName == userName).First().Id;
                DateTime now = DateTime.Now;
                Auction auctionFull = new Auction()
                {
                    UserId = id,
                    CreationDate = now,
                    FinishDate = now.AddDays(auction.DaysDuration),
                    CategoryId = auction.CategoryId,
                    Description = auction.Description,
                    Information = auction.Information,
                    IsNationalCurrency = auction.Currency == "USD" ? false : true,
                    Price = auction.Price,
                    Title = auction.Title
                };

                db.Auctions.Add(auctionFull);
                await db.SaveChangesAsync();
                return RedirectToAction("List", new { userName = userName });
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
            auction.FinishDate = DateTime.Now;
            db.Entry(auction).State = EntityState.Modified;
            await db.SaveChangesAsync();

            AuctionCreateModel auctionShort = new AuctionCreateModel
            {
                AuctionID = auction.AuctionId,
                CategoryId = auction.CategoryId,
                DaysDuration = (auction.FinishDate - auction.CreationDate).Days,
                Title = auction.Title,
                Description = auction.Description,
                Information = auction.Information,
                Price = auction.Price,
                Currency = auction.IsNationalCurrency 
                ? Currency.UAH.ToString() : Currency.USD.ToString()

            };

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auctionShort);
        }

        // POST: Auction/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AuctionCreateModel auction)
        {
            Auction auctionFull = await db.Auctions.FindAsync(auction.AuctionID);
            if (auction == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                auctionFull.Title = auction.Title;
                auctionFull.FinishDate = DateTime.Now.AddDays(auction.DaysDuration);
                auctionFull.CategoryId = auction.CategoryId;
                auctionFull.Description = auction.Description;
                auctionFull.Information = auction.Information;
                auctionFull.IsNationalCurrency = auction.Currency == "USD" ? false : true;
                auctionFull.Price = auction.Price;
               

                db.Entry(auctionFull).State = EntityState.Modified;
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
        [HttpPost, ActionName("FinishConfirmed")]
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
