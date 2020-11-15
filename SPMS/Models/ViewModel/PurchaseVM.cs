using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Models.ViewModel
{
    public class PurchaseVM
    {
        public PurchaseDetail PurchaseDetail { get; set; }

        public PurchaseItems PurchaseItems { get; set; }

        public List<PurchaseItems> PurchaseItemsList { get; set; }

        public IEnumerable<SelectListItem> VendorList { get; set; }

        public IEnumerable<SelectListItem> ItemsList { get; set; }
    }
}
