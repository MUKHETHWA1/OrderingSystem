using Microsoft.EntityFrameworkCore;
using SharedData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<OrderEvent> orderEvents => Set<OrderEvent>();
        public DbSet<ProcessedOrder> processedOrders => Set<ProcessedOrder>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<OrderEvent>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.OrderId);

            });
            mb.Entity<ProcessedOrder>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.OrderId);
            });
        }
    }
}
