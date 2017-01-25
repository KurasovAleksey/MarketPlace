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
using System.IO;
using System.Drawing;
using Microsoft.AspNet.Identity.Owin;
using MarketPlace.WebUI.Models.AccountModels.Utils;

namespace MarketPlace.WebUI.Controllers
{
    public class AuctionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // Users Auctions
        // GET: Auction
        [AllowAnonymous]
        public ActionResult List(string user, int? category, string search, int page = 1)
        {
            int pageSize = 10;

            ViewBag.User = user;
            ViewBag.Search = search;
            ViewBag.Category = category;

            IEnumerable<Auction> auctions = db.Auctions
                .Include(a => a.User)
                .Include(a => a.Category);

            if (!String.IsNullOrEmpty(user))
                auctions = auctions.Where(a => a.User.UserName == user);
            if (category != null && category != 0)
                auctions = auctions.Where(a => a.CategoryId == category);
            if (!String.IsNullOrEmpty(search))
                auctions = auctions.Where(a => a.Title.Contains(search));
            IEnumerable<Auction> auctionsPerPage = auctions.Skip((page - 1) * pageSize).Take(pageSize);

            List<Category> categories = db.Categories.ToList();
            categories.Insert(0, new Category { Title = "Все", CategoryId = 0 });

            PageInfo pageInfo = new PageInfo
            { PageNumber = page, PageSize = pageSize, TotalItems = auctions.ToList().Count };

            AuctionListViewModel alvm = new AuctionListViewModel()
            {
                Auctions = auctionsPerPage,
                Categories = new SelectList(categories, "CategoryId", "Title", category),
                PageInfo = pageInfo
            };

            return View(alvm);
        }

        [Authorize(Roles = "User")]
        public async Task<ActionResult> History(string userName, int page = 1)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(userName);
            if (user == null) return HttpNotFound();

            int pageSize = 10;

            ViewBag.User = user;

            IEnumerable<Bid> bids = db.Bids.Include(b => b.Auction);

            bids = from bid in bids
                                    where bid.UserId == user.Id
                                    group bid by bid.AuctionId
                                    into groups
                                    select groups.OrderByDescending(b => b.Time).FirstOrDefault();

            IEnumerable<Bid> bidsPerPage = bids.OrderByDescending(b => b.Time).Skip((page - 1) * pageSize).Take(pageSize);

            AuctionHistoryModel ahm = new AuctionHistoryModel();
            ahm.Auctions = new List<Pair>();

            foreach (var bid in bidsPerPage)
            {
                ahm.Auctions.Add(new Pair() { Auction = bid.Auction, IsWin = bid.IsFinalBid });
            }

            PageInfo pageInfo = new PageInfo
            { PageNumber = page, PageSize = pageSize, TotalItems = bids.ToList().Count };

            ahm.pageInfo = pageInfo;

            return View(ahm);
        }

        // GET: Auction/Details/5
        [AllowAnonymous]
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
            foreach (var bid in auction.Bids)
                db.Entry(bid).Reference("User").Load();
            ViewBag.Currency = (auction.IsNationalCurrency) 
                ? Currency.UAH.ToString() 
                : Currency.USD.ToString();
            ViewBag.AuctionId = id;
            return View(auction);
        }

        public FilePathResult GetImage(int Auction)
        {
            Auction auction = db.Auctions
                .FirstOrDefault(a => a.AuctionId == Auction);

            if (auction != null)
            {
                return File(auction.PicturePath, auction.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Auction/Create
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Create(AuctionCreateModel auction, HttpPostedFileBase uploadImage = null)
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

                if(uploadImage != null)
                {
                    string filePath = "~/Files/" + System.IO.Path.GetFileName(uploadImage.FileName);
                    auctionFull.PicturePath = filePath;
                    auctionFull.ImageMimeType = uploadImage.ContentType;
                    uploadImage.SaveAs(Server.MapPath(filePath));
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(filePath);
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bmp.Save(Server.MapPath(filePath));
                }

                db.Auctions.Add(auctionFull);
                await db.SaveChangesAsync();
                return RedirectToAction("List", new { user = userName });
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auction);
        }

        // GET: Auction/Edit/5
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Edit(AuctionCreateModel auction, HttpPostedFileBase image = null)
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

                if (image != null)
                {
                    if (auctionFull.PicturePath != null)
                    {
                        FileInfo fi = new FileInfo(auctionFull.PicturePath);
                        if (fi.Exists) fi.Delete();
                    }

                    string filePath = "~/Files/" + System.IO.Path.GetFileName(image.FileName);
                    auctionFull.PicturePath = filePath;
                    auctionFull.ImageMimeType = image.ContentType;
                    image.SaveAs(Server.MapPath(filePath));
                    //Bitmap bmp = (Bitmap)Bitmap.FromFile(Server.MapPath(filePath));
                    //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    //bmp.Save(Server.MapPath(filePath));
                }

                db.Entry(auctionFull).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = auctionFull.AuctionId });
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", auction.CategoryId);
            return View(auction);
        }

        // GET: Auction/Delete/5
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult> FinishConfirmed(int id)
        {
            Auction auction = await db.Auctions.FindAsync(id);
            auction.FinishDate = DateTime.Now;
            db.Entry(auction).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("List", new { user = User.Identity.Name });
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
