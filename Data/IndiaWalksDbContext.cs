using IndiaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IndiaWalks.API.Data
{
    public class IndiaWalksDbContext: DbContext
    {
        // DbCOntext with explicitly mentioning the type of DbContext
        public IndiaWalksDbContext(DbContextOptions<IndiaWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        /* //DbContext With the type of DbContext not defined. This can be used when there is only one DbContext(DB Connection) in case multiple connection it must be mentioned explicitly
        public IndiaWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
            
        }
        */

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("1c19ce39-a066-4403-9ae9-3888c8d79896"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("602be6c3-6831-4ea3-8add-81f4220125ea"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("06d2d285-c956-433f-becf-0621478fb49b"),
                    Name = "Hard"
                }
            };

            //Seed difficulties to Database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("297919e0-8f8a-4fdd-a43e-93fb4eea1792"),
                    Name = "Palamaner",
                    Code = "PLMNR",
                    RegionImageUrl = "https://image2url.com/r2/bucket3/images/1767577638823-e7ed803c-5deb-4595-bfc2-f237749b3c1e.jpg"
                },
                 new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Pungnur",
                    Code = "PGNR",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Madanapalli",
                    Code = "MPL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Kadapa",
                    Code = "KDP",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Chittoor",
                    Code = "CTR",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Puttoor",
                    Code = "PTR",
                    RegionImageUrl = null
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
