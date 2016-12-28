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

        public int FeedbackSenderId { get; set; }
        [ForeignKey("FeedbackSenderId")]
        public ApplicationUser FeedbackSender { get; set; }

        public int FeedbackReceiverId { get; set; }
        [ForeignKey("FeedbackReceiverId")]
        public ApplicationUser FeedbackReceiver { get; set; }
    }
}