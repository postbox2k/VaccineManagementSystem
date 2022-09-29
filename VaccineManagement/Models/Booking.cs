using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineManagement.Models
{
    public class Booking
    {
        [Display(Name = "Id")]
        [Required]
        [Range(7101, 7700)]
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name ")]
        [Required(ErrorMessage = "Please Enter your Name")]
        public string Name { get; set; }
        [Required]
        public int age{ get; set; }
        [RegularExpression("Male|Female|Others")]
        public string Gender { get; set; }
        public string city { get; set; }
        [Required]
        public DateTime Schedule { get; set; }
        [Column(Order = 1)]
        [ForeignKey("Account")]
        public int AID { get; set; }
    }
}
