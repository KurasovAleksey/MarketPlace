using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class Feedback
    {
        public Feedback()
        {
            Datetime = DateTime.Now;
        }

        [Key]
        public int FeedbackId { get; set; }

        [Required(ErrorMessage ="Напишите отзыв")]
        [MaxLength(300)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Отзыв")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Время")]
        public DateTime Datetime { get; set; }

        public int FeedbackSenderId { get; set; }
        [ForeignKey("FeedbackSenderId")]
        public ApplicationUser FeedbackSender { get; set; }

        public int FeedbackReceiverId { get; set; }
        [ForeignKey("FeedbackReceiverId")]
        public ApplicationUser FeedbackReceiver { get; set; }
    }
}