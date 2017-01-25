using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.ViewModels
{
    public class Pair
    {
        public Auction Auction { get; set; }
        public bool IsWin { get; set; }
    }

    public class AuctionHistoryModel
    {
        public List<Pair> Auctions;
        public PageInfo pageInfo;
    }
}