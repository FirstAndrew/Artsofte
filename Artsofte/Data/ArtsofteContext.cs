using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Artsofte.Models;

namespace Artsofte.Data
{
    public class ArtsofteContext : DbContext
    {
        public ArtsofteContext(DbContextOptions<ArtsofteContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Artsofte.Models.Employee> Employee { get; set; } = default!;
        public DbSet<Artsofte.Models.Department> Department { get; set; } = default!;
        public DbSet<Artsofte.Models.Binder> Binder { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // инициализация мзданий, указывать ид обязательно
            modelBuilder
                .Entity<Department>()
                .HasData(
                    new { Id = 1, Name = "Department of development", Floor = 4 },
                    new { Id = 2, Name = "Department of Management", Floor = 7 },
                    new { Id = 3, Name = "Department of Researches", Floor = 6 },
                    new { Id = 4, Name = "Department of Forecast", Floor = 5 },
                    new { Id = 5, Name = "Department of Accounting", Floor = 4 }
            );
        }
    }
}
