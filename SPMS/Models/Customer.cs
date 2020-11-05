using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }

        [Required]
        [Display(Name ="Customer Name")]
        public string Customer_Name { get; set; }

        [Required]
        [Display(Name = "Customer No")]
        public string Customer_No { get; set; }
    }
}
