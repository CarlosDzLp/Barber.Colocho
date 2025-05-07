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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Data.Tables.Version>().Property(m => m.VersionApi).HasPrecision(10, 2);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Roles>().HasData(new Entities.Tables.Roles
            //{
            //    Created = DateTime.UtcNow,
            //    Delete = null,
            //    Id = 1,
            //    IsActive = true,
            //    RolName = Enums.RolesEnum.User,
            //    Updated = null
            //});
        }
        public DbSet<Data.Tables.Version> Version { get; set; }
    }
}
