using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class AuctionListViewModel
    {
        public IEnumerable<Auction> Auctions { get; set; }
        public SelectList Categories { get; set; }
    }
}