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

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }

        public DbSet<SaleDetail> SaleDetail { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<PurchaseItems> PurchaseItems { get; set; }

        public DbSet<SaleItems> SaleItems { get; set; }
    }
}
