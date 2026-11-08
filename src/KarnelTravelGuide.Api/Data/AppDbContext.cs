using KarnelTravelGuide.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<SystemLog> SystemLogs => Set<SystemLog>();
    public DbSet<EmailLog> EmailLogs => Set<EmailLog>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<CategoryType> CategoryTypes => Set<CategoryType>();
    public DbSet<TouristSpot> TouristSpots => Set<TouristSpot>();
    public DbSet<TourPackage> TourPackages => Set<TourPackage>();
    public DbSet<Promotion> Promotions => Set<Promotion>();
    public DbSet<Models.ServiceProvider> ServiceProviders => Set<Models.ServiceProvider>();
    public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();
    public DbSet<TransportRoute> TransportRoutes => Set<TransportRoute>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.FullName).HasMaxLength(120);
            entity.Property(x => x.Email).HasMaxLength(160);
            entity.Property(x => x.PasswordHash).HasMaxLength(500);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.Role).HasMaxLength(20);
            entity.Property(x => x.Address).HasMaxLength(250);
            entity.Property(x => x.AvatarUrl).HasMaxLength(300);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");
            entity.Property(x => x.FullName).HasMaxLength(120);
            entity.Property(x => x.Email).HasMaxLength(160);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.Subject).HasMaxLength(200);
            entity.Property(x => x.Message).HasMaxLength(2000);
            entity.Property(x => x.Status).HasMaxLength(20);
            entity.HasOne(x => x.ProcessedByUser)
                .WithMany()
                .HasForeignKey(x => x.ProcessedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedbacks");
            entity.Property(x => x.FullName).HasMaxLength(120);
            entity.Property(x => x.Email).HasMaxLength(160);
            entity.Property(x => x.TargetType).HasMaxLength(30);
            entity.Property(x => x.Content).HasMaxLength(2000);
            entity.Property(x => x.Status).HasMaxLength(20);
            entity.HasOne(x => x.User)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ReviewedByUser)
                .WithMany()
                .HasForeignKey(x => x.ReviewedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.Property(x => x.Title).HasMaxLength(160);
            entity.Property(x => x.Message).HasMaxLength(1000);
            entity.Property(x => x.Type).HasMaxLength(30);
            entity.HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.ToTable("EmailLogs");
            entity.Property(x => x.ToEmail).HasMaxLength(160);
            entity.Property(x => x.Subject).HasMaxLength(200);
            entity.Property(x => x.Body).HasMaxLength(4000);
            entity.Property(x => x.Status).HasMaxLength(20);
            entity.Property(x => x.ErrorMessage).HasMaxLength(1000);
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.ToTable("SystemLogs");
            entity.Property(x => x.Action).HasMaxLength(50);
            entity.Property(x => x.EntityName).HasMaxLength(50);
            entity.Property(x => x.EntityId).HasMaxLength(50);
            entity.Property(x => x.Detail).HasMaxLength(1000);
            entity.Property(x => x.IpAddress).HasMaxLength(50);
            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Locations");
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.City).HasMaxLength(80);
            entity.Property(x => x.Province).HasMaxLength(80);
            entity.Property(x => x.Country).HasMaxLength(80);
            entity.Property(x => x.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.ToTable("CategoryTypes");
            entity.Property(x => x.Name).HasMaxLength(80);
            entity.Property(x => x.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<TouristSpot>(entity =>
        {
            entity.ToTable("TouristSpots");
            entity.Property(x => x.Name).HasMaxLength(160);
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.Property(x => x.Address).HasMaxLength(250);
            entity.Property(x => x.OpenTime).HasMaxLength(80);
            entity.Property(x => x.TicketPrice).HasPrecision(18, 2);
            entity.Property(x => x.ImageUrl).HasMaxLength(300);
            entity.HasOne(x => x.Location)
                .WithMany(x => x.TouristSpots)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.CategoryType)
                .WithMany(x => x.TouristSpots)
                .HasForeignKey(x => x.CategoryTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TourPackage>(entity =>
        {
            entity.ToTable("TourPackages");
            entity.Property(x => x.Name).HasMaxLength(160);
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.Property(x => x.Price).HasPrecision(18, 2);
            entity.Property(x => x.ImageUrl).HasMaxLength(300);
            entity.HasOne(x => x.TouristSpot)
                .WithMany(x => x.TourPackages)
                .HasForeignKey(x => x.TouristSpotId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.ToTable("Promotions");
            entity.HasIndex(x => x.Code).IsUnique().HasFilter("[Code] IS NOT NULL");
            entity.Property(x => x.Title).HasMaxLength(160);
            entity.Property(x => x.Code).HasMaxLength(30);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.DiscountPercent).HasPrecision(5, 2);
            entity.HasOne(x => x.TourPackage)
                .WithMany(x => x.Promotions)
                .HasForeignKey(x => x.TourPackageId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Models.ServiceProvider>(entity =>
        {
            entity.ToTable("ServiceProviders");
            entity.Property(x => x.Name).HasMaxLength(160);
            entity.Property(x => x.Type).HasMaxLength(20);
            entity.Property(x => x.Address).HasMaxLength(250);
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.ImageUrl).HasMaxLength(300);
            entity.HasOne(x => x.Location)
                .WithMany(x => x.ServiceProviders)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ServiceItem>(entity =>
        {
            entity.ToTable("ServiceItems");
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.ItemType).HasMaxLength(20);
            entity.Property(x => x.Price).HasPrecision(18, 2);
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasOne(x => x.ServiceProvider)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ServiceProviderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TransportRoute>(entity =>
        {
            entity.ToTable("TransportRoutes");
            entity.Property(x => x.Name).HasMaxLength(160);
            entity.Property(x => x.FromPlace).HasMaxLength(120);
            entity.Property(x => x.ToPlace).HasMaxLength(120);
            entity.Property(x => x.DistanceKm).HasPrecision(8, 1);
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");
            entity.HasIndex(x => x.PlateNumber).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.VehicleType).HasMaxLength(20);
            entity.Property(x => x.PlateNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedules");
            entity.Property(x => x.Price).HasPrecision(18, 2);
            entity.HasOne(x => x.TransportRoute)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.TransportRouteId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Vehicle)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(20);
            entity.Property(x => x.BookingType).HasMaxLength(20);
            entity.Property(x => x.Status).HasMaxLength(20);
            entity.Property(x => x.TotalAmount).HasPrecision(18, 2);
            entity.Property(x => x.ContactName).HasMaxLength(120);
            entity.Property(x => x.ContactPhone).HasMaxLength(20);
            entity.Property(x => x.ContactEmail).HasMaxLength(160);
            entity.Property(x => x.Note).HasMaxLength(500);
            entity.HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.ToTable("BookingDetails");
            entity.Property(x => x.ItemType).HasMaxLength(30);
            entity.Property(x => x.ItemName).HasMaxLength(160);
            entity.Property(x => x.UnitPrice).HasPrecision(18, 2);
            entity.HasOne(x => x.Booking)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
