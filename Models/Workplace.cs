using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRDN68_SOF_2022231.Models
{
    public class Workplace
    {
        [Key]
        public string Id { get; set; }

        [StringLength(100)]
        [Required]
        public string CompanyName { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [Required]
        public int WorkedYears { get; set; }

        [StringLength(100)]
        [Required]
        public string Role { get; set; }

        public string OwnerId { get; set; }

        [NotMapped]
        public virtual Resume Owner { get; set; }

        public Workplace()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
