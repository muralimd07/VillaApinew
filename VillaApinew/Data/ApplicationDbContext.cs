using Microsoft.EntityFrameworkCore;
using VillaApinew.Modal;

namespace VillaApinew.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        DbSet<Villa> Villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa 
            { Id=1,Name="Royal Villa",Details="details 1",ImageUrl="",Occupancy=5,Rate=200,Sqrt=550,Amenity="",CreateDate=DateTime.Now},
            new Villa
            { Id = 2, Name = "Royal Villa2", Details = "details 2", ImageUrl = "", Occupancy = 5, Rate = 200, Sqrt = 550, Amenity = "", CreateDate = DateTime.Now },
            new Villa
            { Id = 3, Name = "Royal Villa3", Details = "details 3", ImageUrl = "", Occupancy = 5, Rate = 200, Sqrt = 550, Amenity = "" , CreateDate = DateTime.Now }




            );

        }

    }
}
