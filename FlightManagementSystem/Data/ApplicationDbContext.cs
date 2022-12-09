

namespace CabManagementSystem.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Bookings>BookARide{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Bookings>()
                .HasOne(m => m.Users)
                .WithMany(m => m.BookingCabs)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
