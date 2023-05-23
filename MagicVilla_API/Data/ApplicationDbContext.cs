using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumberVilla> NumberVilla { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Villa>().HasData(
            //    new Villa()
            //    {
            //        Id = 1,
            //        Name = "Villa real",
            //        Detail = "Detalles",
            //        ImgUrl = "",
            //        Ocuppants = 123,
            //        SquareMeters = 123,
            //        Price = 123,
            //        Amenity = "",
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //    },
            //    new Villa()
            //    {
            //        Id = 2,
            //        Name = "Villa real312",
            //        Detail = "Detalles312",
            //        ImgUrl = "",
            //        Ocuppants = 64,
            //        SquareMeters = 46,
            //        Price = 567,
            //        Amenity = "",
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //    }

            //    );
        }
    }
}
