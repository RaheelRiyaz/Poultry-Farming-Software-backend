using KashmirPoultrySoftware.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Persistence.Data
{
    public class KashmirPoultrySoftwareDbContext : DbContext
    {
        public KashmirPoultrySoftwareDbContext(DbContextOptions options) : base(options)
        {
            
        }


        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Hatch> Hatches { get; set; } = null!;
        public DbSet<Motality> Motivalities { get; set; } = null!;
        public DbSet<Expenditure> Expenditures { get; set; } = null!;
        public DbSet<AppFile> AppFiles { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
    }
}
