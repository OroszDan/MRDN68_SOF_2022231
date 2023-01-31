using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRDN68_SOF_2022231.Models
{
    public class Stat
    {
        [Display(Name = "First Name")]
        public string YFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string YLastName { get; set; }
        [Display(Name = "Age")]
        public int YAge { get; set; }
        [Display(Name = "First Name")]
        public string EFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string ELastName { get; set; }
        [Display(Name = "Years")]
        public int EYears { get; set; }
    }
}
