using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudDmytro_INCHKIEV_TEST.Models
{
    public class MyDbContext:IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                   // Id = 1,
                    Login = "admin@admin.admin",
                    Pass = "admin",
                    Role = "admin"
                }
            );
        }
        public DbSet<User> MyUsers { get; set; }
       public  DbSet<Book> Books { get; set; }

    }
}
