using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Auction
    {
        public Auction()
        {

        }

        [Key]
        public int AuctionId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(400)]
        public string Information { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [Column("FinishDate", TypeName = "datetime2")]
        public DateTime FinishDate { get; set; }

        ICollection<Bid> Bids { get; set; }
    }
}