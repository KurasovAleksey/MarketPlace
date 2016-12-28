using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    [Table("Auctions")]
    public class Auction : Sale
    {
        public Auction()
        {

        }

        [Column("FinishDate", TypeName = "datetime2")]
        public DateTime FinishDate { get; set; }

        ICollection<Bid> Bids { get; set; }
    }
}