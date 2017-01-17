using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Item
    {
        public Item()
        {
        }

        [Key]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "nvarchar")]
        public string PicturePath { get; set; }

        //public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public ApplicationUser ApplicationUser { get; set; }

        //public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }

        //public ICollection<Auction> Auctions { get; set; }
    }
}