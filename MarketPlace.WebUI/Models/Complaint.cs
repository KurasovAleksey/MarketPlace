using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class Complaint
    {
        public Complaint()
        {
        }

        [Key]
        public int ComplaintId { get; set; }

        [Required]
        [MaxLength(300)]
        [Column(TypeName = "nvarchar")]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Datetime { get; set; }

        [Column("BanStatus", TypeName = "bit")]
        public bool isProcessed { get; set; }

        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public int ViolatorId { get; set; }
        [ForeignKey("ViolatorId")]
        public ApplicationUser Violator { get; set; }

    }
}