using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineManagement.Models
{
    public class Account
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "username ")]
        [Required(ErrorMessage = "Please enter valid username")]
        public string username { get; set; }
        [Required]

        public string password { get; set; }
    }
}
