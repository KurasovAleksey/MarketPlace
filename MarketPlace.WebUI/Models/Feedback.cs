using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Feedback
    {
        public Feedback()
        {
        }

        [Key]
        public int FeedbackId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Comment { get; set; }

        [Required]
        public DateTime Datetime { get; set; }

        public int UserFromId { get; set; }
        [ForeignKey("UserFromId")]
        public ApplicationUser Sender { get; set; }

        public int UserToId { get; set; }
        [ForeignKey("UserToId")]
        public ApplicationUser Receiver { get; set; }
    }
}