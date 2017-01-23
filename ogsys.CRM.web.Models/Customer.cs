using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ogsys.CRM.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name ="First Name")]
        public string Firstname { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [StringLength(40)]
        public string Company { get; set; }

        [Required]
        [StringLength(254)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(40)]
        public string Address { get; set; }

        [StringLength(24)]
        [Required(ErrorMessage = "Phone no. is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }
        public virtual ICollection<CustomerNote> Notes { get; set; }
    }


  

}