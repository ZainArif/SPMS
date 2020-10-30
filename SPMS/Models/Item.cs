using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models
{
    public class Item
    {
        [Key]
        public int Item_Id { get; set; }

        [Required]
        public string Item_Name { get; set; }

        [Required]
        public string Item_Code { get; set; }

    }
}
