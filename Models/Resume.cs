using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MRDN68_SOF_2022231.Models
{
    public class Resume
    {
        [Key]
        public string Id { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        public string Description { get; set; }

        //public string? ContentType { get; set; }

        //public byte[]? Data { get; set; }

        public string? OwnerId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual IdentityUser? Owner { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Workplace> Workplaces { get; set; }

        public Resume()
        {
            Workplaces = new HashSet<Workplace>();
            Id = Guid.NewGuid().ToString();
        }
    }
}
