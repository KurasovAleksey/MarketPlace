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
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(400)]
        [Display(Name = "Доп.информация")]
        public string Information { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Начало")]
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
        [Display(Name = "Окончание")]
        public DateTime FinishDate { get; set; }

       
        [Column("IsFinished", TypeName = "bit")]
        [Display(Name = "Завершен")]
        public bool IsFinished { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}