using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Barber.Colocho.Infraestructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid guid = Guid.Parse("2b921c40-7644-4b5e-b1e1-c79767fbe940");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Data.Tables.Version>().Property(m => m.VersionApi).HasPrecision(10, 2);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Data.Tables.Version>().HasData(new Data.Tables.Version
            //{
            //    Created = DateTime.UtcNow,
            //    Deleted = null,
            //    Id = guid,
            //    IsActive = true,
            //    Updated = null,
            //    VersionApi = 1.0
            //});
            modelBuilder.Entity<Data.Tables.Geolocator>().Property(u => u.Location)
            .HasColumnType("geography");

            modelBuilder.Entity<Data.Tables.CompanyAddress>().Property(u => u.Location)
                .HasColumnType("geography");
        }
        public DbSet<Data.Tables.Version> Version { get; set; }
        public DbSet<Data.Tables.User> User { get; set; }
        public DbSet<Data.Tables.Geolocator> Geolocator { get; set; }
        public DbSet<Data.Tables.Code> Code { get; set; }
        public DbSet<Data.Tables.Password> Password { get; set; }
        public DbSet<Data.Tables.Company> Company { get; set; }
        public DbSet<Data.Tables.CompanyAddress> CompanyAddress { get; set; }
        public DbSet<Data.Tables.CompanyImage> CompanyImage { get; set; }
        public DbSet<Data.Tables.Service> Service { get; set; }
        public DbSet<Data.Tables.ServiceImage>  ServiceImage { get; set; }
    }
}
