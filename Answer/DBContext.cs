using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Answer
{
    public partial class DBContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=entitycore;Username=postgres;Password=mypassword");
        }
    }
}
