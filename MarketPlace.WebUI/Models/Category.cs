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
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; }
        
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public ICollection<Category> Childrens { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}