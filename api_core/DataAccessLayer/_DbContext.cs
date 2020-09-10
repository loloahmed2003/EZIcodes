
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class _DbContext : DbContext
    {
        public _DbContext(DbContextOptions<_DbContext> op) : base(op)
        { }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(User => User.contact)
                .WithOne(contact => contact.user)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            });

           
        }
    }
}