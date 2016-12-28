using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Bid
    {
        public Bid()
        {
        }

        [Key]
        public int BidId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Column("Datetime", TypeName = "datetime2")]
        public DateTime Datetime { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser Gamer { get; set; }

        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }


    }
}