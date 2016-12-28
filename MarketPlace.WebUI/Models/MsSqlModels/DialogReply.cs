using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    public class DialogReply
    {
        public DialogReply()
        {
        }

        [Key]
        public int DialogReplyId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Datetime { get; set; }

        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public int DialogId { get; set; }
        [ForeignKey("DialogId")]
        public Dialog Dialog { get; set; }
    }
}