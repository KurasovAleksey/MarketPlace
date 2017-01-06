using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Sale
    {
        public Sale()
        {
        }

        [Key]
        public int SaleId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(400)]
        public string Information { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "bit")]
        public bool isClosed { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        
    }
}