using Barber.Colocho.Entities.Tables;
using Microsoft.EntityFrameworkCore;

namespace Barber.Colocho.DataAccess.Db
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Roles>().HasData(new Entities.Tables.Roles
            //{
            //    Created = DateTime.UtcNow,
            //    Delete = null,
            //    Id = 1,
            //    IsActive = true,
            //    RolName = Enums.RolesEnum.User,
            //    Updated = null
            //});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Code> Code { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Colony> Colony { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<VersionApi> VersionApi { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ImageService> ImageService { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<RatingService> RatingService { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Suscription> Suscription { get; set; }
        public DbSet<Error> Error { get; set; }
    }
}
