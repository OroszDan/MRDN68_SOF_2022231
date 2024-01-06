using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MRDN68_SOF_2022231.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

    }
}
