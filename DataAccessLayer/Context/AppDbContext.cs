using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<Favorite>()
            .HasKey(f => f.Id);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Set the delete behavior

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Unit)
                .WithMany()
                .HasForeignKey(f => f.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<UnitCategory> unitCategories { get; set; }
		public DbSet<Unit>  units{ get; set; }
		public DbSet<Comment> comments { get; set; }
		public DbSet<Favorite> favorites { get; set; }
		public DbSet<ScheduleAppointment> scheduleAppointment { get; set; }


    }
}
