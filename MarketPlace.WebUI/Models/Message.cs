using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Message
    {
        public Message()
        {
        }

        [Key]
        public int MessageId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Subject { get; set; }

        [Required]
        [Column("Text", TypeName = "varchar")]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Column("Datetime", TypeName = "datetime2")]
        public DateTime Datetime { get; set; }


        public int UserFromId { get; set; }
        [ForeignKey("UserFromId")]
        public ApplicationUser Sender { get; set; }

        public int UserToId { get; set; }
        [ForeignKey("UserToId")]
        public ApplicationUser Receiver { get; set; }
    }
}