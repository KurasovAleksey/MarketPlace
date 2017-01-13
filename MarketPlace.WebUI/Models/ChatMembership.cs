using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class CharMembership
    {
        public CharMembership()
        {
        }

        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Key, Column(Order = 1)]
        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }


        public bool WasRemoved { get; set; }

        public string ChatName { get; set; }



    }
}