using EVSDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSDAL
{
    public class EVSDbContext : DbContext
    {
        static public bool InMemory { get; set; }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .ToTable("Companies")
                .Property(x => x.Branches)
                .HasConversion(x => String.Join('|', x),
                                x => x.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (InMemory)
                optionsBuilder.UseInMemoryDatabase("EVSDatabase");
            else
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=[AbsoluteFolderPath]\\EVSDatabase.mdf;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
