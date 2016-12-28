using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class Dialog
    {
        [Key]
        public int DialogId { get; set; }

        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser Creator { get; set; }

        public int GuestId { get; set; }
        [ForeignKey("GuestId")]
        public ApplicationUser Guest { get; set; }

        public ICollection<DialogReply> Replies { get; set; }
    }
}