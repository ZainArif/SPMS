using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class Vendor
    {
        [Key]
        public int Vender_Id { get; set; }

        [Required]
        [Display(Name = "Vendor")]
        public string Vender_Name { get; set; }

        [Required]
        [Display(Name = "Contact Person Name")]
        public string Contact_Person_Name { get; set; }

        [Required]
        [Display(Name = "Contact Person No")]
        public string Contect_Person_No { get; set; }
    }
}
