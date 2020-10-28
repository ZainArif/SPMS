using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class PurchaseDetail
    {
        [Key]
        public int Purchase_Id { get; set; }

        [Required]
        public int Vender_Id { get; set; }

        [ForeignKey("Vender_Id")]
        public Vendor Vendor { get; set; }

        [Required]
        public DateTime Purchase_Date { get; set; }

        [Required]
        public string Purchase_Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Unit_Cost { get; set; }

        [Required]
        public int Total_Cost { get; set; }

        [Required]
        public int Amount_Paid { get; set; }
        
        [Required]
        public int Amount_Balance { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
