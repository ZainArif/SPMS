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
        public int Sale_Id { get; set; }

        [Required]
        public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        public Customer Customer { get; set; }

        [Required]
        public DateTime Sale_Date { get; set; }

        [Required]
        public int Amount_Received { get; set; }

        [Required]
        public int Amount_Balance { get; set; }

        [Required]
        public int Expense { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
