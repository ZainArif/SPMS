using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class PurchaseItems
    {
        [Key]
        public int Purchase_Item_Id { get; set; }

        [Required]
        public int Purchase_Id { get; set; }

        [ForeignKey("Purchase_Id")]
        public PurchaseDetail PurchaseDetail { get; set; }

        [Required]
        public int Item_Id { get; set; }

        [ForeignKey("Item_Id")]
        public Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int Available_Quantity { get; set; }

        [Required]
        public int Unit_Cost { get; set; }

        [Required]
        public int Total_Cost { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
