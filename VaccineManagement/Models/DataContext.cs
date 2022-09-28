using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VaccineManagement.Models
{
    public class DataContext: DbContext
    {
        ///method for database binding
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Booking>().HasNoKey();
            //modelBuilder.Entity<Account>().HasNoKey();
        }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:VacMgt"]);

            //DataSet Table



        }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Account> Account { get; set; }

    }
}
