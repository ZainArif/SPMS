using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Sale No")]
        public int Sale_Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Customer field is required.")]
        public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        public Customer Customer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Sale Date")]
        public DateTime Sale_Date { get; set; }

        [Required]
        [Display(Name = "Amount Received")]
        public int Amount_Received { get; set; }

        [Required]
        [Display(Name = "Amount Balance")]
        public int Amount_Balance { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
