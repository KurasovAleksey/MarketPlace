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
        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(400)]
        public string Information { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "nvarchar")]
        public string PicturePath { get; set; }

        public string ImageMimeType { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Column("IsNationalCurrency")]
        public bool IsNationalCurrency { get; set; }

        [Required]
        [Column("FinishDate", TypeName = "datetime2")]
        public DateTime FinishDate { get; set; }

       
        [Column("IsFinished", TypeName = "bit")]
        public bool IsFinished { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}