using Microsoft.EntityFrameworkCore;
using Stock.Core.Entites;
using Stock.Core.Entites.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository.Data
{
    public class StockDbContext:DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }
    
}
