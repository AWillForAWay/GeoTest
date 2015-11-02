using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocacheApp2.Models
{
    public class Geocache
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+", ErrorMessage = "Only letters, numbers and spaces are allowed")]
        [StringLength(50, ErrorMessage = "Name max length is 50")]
        [Display(Name ="Geocache Name")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.######}")]
        public decimal Latitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.######}")]
        public decimal Longitude { get; set; }
    }
}
