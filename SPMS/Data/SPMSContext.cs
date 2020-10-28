using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Models;

namespace SPMS.Data
{
    public class SPMSContext : DbContext
    {
        public SPMSContext (DbContextOptions<SPMSContext> options)
            : base(options)
        {
        }

        public DbSet<SPMS.Models.Vendor> Vendor { get; set; }

        public DbSet<SPMS.Models.PurchaseDetail> PurchaseDetail { get; set; }

        public DbSet<SPMS.Models.SaleDetail> SaleDetail { get; set; }
    }
}
