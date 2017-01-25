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

        [Required(ErrorMessage = "Введите размер ставки")]
        [Display(Name = "Размер ставки")]
        [Range(0.02, 1000000000)]
        public decimal Amount { get; set; }

        [Column("Time", TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Display(Name = "Победитель")]
        [Column("IsFinalBid", TypeName = "bit")]
        public bool IsFinalBid { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }


    }
}