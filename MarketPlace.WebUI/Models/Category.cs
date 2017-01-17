using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Category
    {
        public Category()
        {
            Children = new List<Category>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Пустое значение недопустимо")]
        [MaxLength(30, ErrorMessage = "Превышена допустимая длина строки")]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Пустое значение недопустимо")]
        [MaxLength(100, ErrorMessage = "Превышена допустимая длина строки")]
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; }
        
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }

        public ICollection<Auction> Auctions { get; set; }

    }
}