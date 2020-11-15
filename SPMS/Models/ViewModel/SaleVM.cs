using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models.ViewModel
{
    public class SaleVM
    {
        public SaleDetail SaleDetail { get; set; }

        public SaleItems SaleItems { get; set; }

        public List<SaleItems> SaleItemsList { get; set; }

        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}
