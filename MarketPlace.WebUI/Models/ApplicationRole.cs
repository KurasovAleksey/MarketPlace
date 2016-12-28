using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationRole : 
        IdentityRole<int, ApplicationUserRole>, 
        IRole<int>
    {
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        public ApplicationRole() { }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }
    }
}