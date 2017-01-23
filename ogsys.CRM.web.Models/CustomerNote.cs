using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ogsys.CRM.Models
{
    public class CustomerNote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Body { get; set; }

        [Display(Name ="Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }

        public int CustomerId { get; set; }

    }
}