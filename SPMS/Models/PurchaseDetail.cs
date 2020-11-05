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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Purchase No")]
        public int Purchase_Id { get; set; }

        [Required]
        public int Vender_Id { get; set; }

        [ForeignKey("Vender_Id")]
        public Vendor Vendor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase Date")]
        public DateTime Purchase_Date { get; set; }

        [Display(Name = "Purchase Description")]
        public string Purchase_Description { get; set; }

        [Required]
        [Display(Name = "Amount Paid")]
        public int Amount_Paid { get; set; }
        
        [Required]
        [Display(Name = "Amount Balance")]
        public int Amount_Balance { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
