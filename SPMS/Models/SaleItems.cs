using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class SaleItems
    {
        [Key]
        public int Sale_Item_Id { get; set; }

        [Required]
        public int Sale_Id { get; set; }

        [ForeignKey("Sale_Id")]
        public SaleDetail SaleDetail { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Item field is required.")]
        public int Purchase_Item_Id { get; set; }

        [ForeignKey("Purchase_Item_Id")]
        public PurchaseItems PurchaseItems { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Unit Price field is required.")]
        public int Unit_Price { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Total Price field is required.")]
        public int Total_Price { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
