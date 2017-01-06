using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        public string Preview { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        public string Info { get; set; }

        public ICollection<CharMembership> UserChats { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}