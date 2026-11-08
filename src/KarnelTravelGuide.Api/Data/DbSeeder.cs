using System.Security.Cryptography;
using System.Text;
using KarnelTravelGuide.Api.Models;
using ServiceProvider = KarnelTravelGuide.Api.Models.ServiceProvider;

namespace KarnelTravelGuide.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Users.Any())
        {
            return;
        }

        var now = new DateTime(2026, 11, 8, 8, 0, 0, DateTimeKind.Utc);

        var admin = new User
        {
            FullName = "Quản trị Karnel",
            Email = "admin@karnel.local",
            PasswordHash = HashPassword("Admin@123"),
            Phone = "0901000001",
            Role = "Admin",
            Address = "Hải Châu, Đà Nẵng",
            IsActive = true,
            CreatedAt = now
        };
        var user = new User
        {
            FullName = "Nguyễn Minh An",
            Email = "user@karnel.local",
            PasswordHash = HashPassword("User@123"),
            Phone = "0901000002",
            Role = "User",
            Address = "Sơn Trà, Đà Nẵng",
            IsActive = true,
            CreatedAt = now
        };
        db.Users.AddRange(admin, user);

        var locations = new[]
        {
            new Location { Name = "Đà Nẵng", City = "Đà Nẵng", Province = "Đà Nẵng", Description = "Thành phố biển miền Trung." },
            new Location { Name = "Hội An", City = "Hội An", Province = "Quảng Nam", Description = "Phố cổ và sông Hoài." },
            new Location { Name = "Huế", City = "Huế", Province = "Thừa Thiên Huế", Description = "Cố đô và di tích triều Nguyễn." },
            new Location { Name = "Nha Trang", City = "Nha Trang", Province = "Khánh Hòa", Description = "Vịnh biển và đảo." }
        };
        db.Locations.AddRange(locations);

        var categories = new[]
        {
            new CategoryType { Name = "Biển", Description = "Điểm đến ven biển." },
            new CategoryType { Name = "Di tích", Description = "Di tích lịch sử, văn hóa." },
            new CategoryType { Name = "Núi", Description = "Khu du lịch núi và cao nguyên." },
            new CategoryType { Name = "Ẩm thực", Description = "Điểm ăn uống đặc sản." }
        };
        db.CategoryTypes.AddRange(categories);
        db.SaveChanges();

        var spots = new[]
        {
            new TouristSpot { Name = "Bà Nà Hills", LocationId = locations[0].Id, CategoryTypeId = categories[2].Id, Description = "Khu du lịch trên núi với cầu Vàng.", Address = "Hòa Ninh, Hòa Vang, Đà Nẵng", OpenTime = "07:00-22:00", TicketPrice = 900000, ImageUrl = "/images/ba-na.jpg", IsActive = true },
            new TouristSpot { Name = "Biển Mỹ Khê", LocationId = locations[0].Id, CategoryTypeId = categories[0].Id, Description = "Bãi biển trung tâm Đà Nẵng.", Address = "Phước Mỹ, Sơn Trà, Đà Nẵng", OpenTime = "Cả ngày", TicketPrice = 0, ImageUrl = "/images/my-khe.jpg", IsActive = true },
            new TouristSpot { Name = "Phố cổ Hội An", LocationId = locations[1].Id, CategoryTypeId = categories[1].Id, Description = "Khu phố cổ được UNESCO công nhận.", Address = "Minh An, Hội An", OpenTime = "Cả ngày", TicketPrice = 120000, ImageUrl = "/images/hoi-an.jpg", IsActive = true },
            new TouristSpot { Name = "Đại Nội Huế", LocationId = locations[2].Id, CategoryTypeId = categories[1].Id, Description = "Hoàng thành triều Nguyễn.", Address = "Đông Ba, Huế", OpenTime = "07:00-17:30", TicketPrice = 200000, ImageUrl = "/images/dai-noi.jpg", IsActive = true },
            new TouristSpot { Name = "Vịnh Nha Trang", LocationId = locations[3].Id, CategoryTypeId = categories[0].Id, Description = "Tham quan vịnh và các đảo.", Address = "Trần Phú, Nha Trang", OpenTime = "07:00-17:00", TicketPrice = 350000, ImageUrl = "/images/nha-trang.jpg", IsActive = true },
            new TouristSpot { Name = "Chợ Hội An", LocationId = locations[1].Id, CategoryTypeId = categories[3].Id, Description = "Đặc sản và ẩm thực địa phương.", Address = "Trần Phú, Hội An", OpenTime = "06:00-18:00", TicketPrice = 0, ImageUrl = "/images/cho-hoi-an.jpg", IsActive = true }
        };
        db.TouristSpots.AddRange(spots);
        db.SaveChanges();

        var tours = new[]
        {
            new TourPackage { Name = "Đà Nẵng 2 ngày", TouristSpotId = spots[0].Id, Description = "Bà Nà Hills và biển Mỹ Khê.", DurationDays = 2, Price = 2500000, MaxGuests = 20, ImageUrl = "/images/tour-danang.jpg", IsActive = true },
            new TourPackage { Name = "Hội An về đêm", TouristSpotId = spots[2].Id, Description = "Phố cổ và đèn lồng.", DurationDays = 1, Price = 890000, MaxGuests = 15, ImageUrl = "/images/tour-hoian.jpg", IsActive = true },
            new TourPackage { Name = "Huế di sản", TouristSpotId = spots[3].Id, Description = "Đại Nội và lăng tẩm.", DurationDays = 2, Price = 2100000, MaxGuests = 18, ImageUrl = "/images/tour-hue.jpg", IsActive = true },
            new TourPackage { Name = "Nha Trang đảo", TouristSpotId = spots[4].Id, Description = "Tour vịnh trong ngày.", DurationDays = 1, Price = 750000, MaxGuests = 30, ImageUrl = "/images/tour-nhatrang.jpg", IsActive = true }
        };
        db.TourPackages.AddRange(tours);

        var providers = new[]
        {
            new ServiceProvider { Name = "Karnel Beach Hotel", Type = "Hotel", LocationId = locations[0].Id, Address = "Võ Nguyên Giáp, Đà Nẵng", Description = "Khách sạn view biển.", Phone = "02361111001", StarRating = 4, ImageUrl = "/images/hotel-beach.jpg", IsActive = true },
            new ServiceProvider { Name = "Hội An Riverside Hotel", Type = "Hotel", LocationId = locations[1].Id, Address = "Nhiêu Lộc, Hội An", Description = "Khách sạn sát phố cổ.", Phone = "02351111002", StarRating = 3, ImageUrl = "/images/hotel-hoian.jpg", IsActive = true },
            new ServiceProvider { Name = "Nhà hàng Mỳ Quảng Bà", Type = "Restaurant", LocationId = locations[0].Id, Address = "Trần Quốc Toản, Đà Nẵng", Description = "Đặc sản mì Quảng.", Phone = "09031111003", StarRating = null, ImageUrl = "/images/restaurant.jpg", IsActive = true },
            new ServiceProvider { Name = "Karnel Bay Resort", Type = "Resort", LocationId = locations[3].Id, Address = "Trần Phú, Nha Trang", Description = "Resort sát vịnh.", Phone = "02581111004", StarRating = 5, ImageUrl = "/images/resort.jpg", IsActive = true }
        };
        db.ServiceProviders.AddRange(providers);

        var routes = new[]
        {
            new TransportRoute { Name = "Đà Nẵng - Hội An", FromPlace = "Đà Nẵng", ToPlace = "Hội An", DistanceKm = 30, Description = "Tuyến xe dọc biển.", IsActive = true },
            new TransportRoute { Name = "Đà Nẵng - Huế", FromPlace = "Đà Nẵng", ToPlace = "Huế", DistanceKm = 100, Description = "Tuyến đèo Hải Vân.", IsActive = true },
            new TransportRoute { Name = "Đà Nẵng - Nha Trang", FromPlace = "Đà Nẵng", ToPlace = "Nha Trang", DistanceKm = 530, Description = "Tuyến đường dài.", IsActive = true }
        };
        db.TransportRoutes.AddRange(routes);

        var vehicles = new[]
        {
            new Vehicle { Name = "Xe 16 chỗ Karnel", VehicleType = "Car", PlateNumber = "43A-10001", SeatCount = 16, IsActive = true },
            new Vehicle { Name = "Xe giường nằm", VehicleType = "Bus", PlateNumber = "43B-20002", SeatCount = 40, IsActive = true },
            new Vehicle { Name = "Tàu SE", VehicleType = "Train", PlateNumber = "SE-01", SeatCount = 200, IsActive = true }
        };
        db.Vehicles.AddRange(vehicles);
        db.SaveChanges();

        db.Promotions.AddRange(
            new Promotion { Title = "Giảm tour Đà Nẵng", Code = "DANANG10", Description = "Giảm 10% tour Đà Nẵng 2 ngày.", DiscountPercent = 10, StartDate = now.Date, EndDate = now.Date.AddMonths(2), TourPackageId = tours[0].Id, IsActive = true },
            new Promotion { Title = "Hội An cuối tuần", Code = "HOIAN15", Description = "Giảm 15% tour Hội An về đêm.", DiscountPercent = 15, StartDate = now.Date, EndDate = now.Date.AddMonths(1), TourPackageId = tours[1].Id, IsActive = true },
            new Promotion { Title = "Ưu đãi mở bán", Code = "WELCOME5", Description = "Giảm 5% cho đơn đầu tiên.", DiscountPercent = 5, StartDate = now.Date, EndDate = now.Date.AddMonths(3), TourPackageId = null, IsActive = true }
        );

        db.ServiceItems.AddRange(
            new ServiceItem { ServiceProviderId = providers[0].Id, Name = "Phòng Deluxe", ItemType = "Room", Capacity = 2, Price = 1500000, Description = "View biển, 1 giường lớn.", IsAvailable = true },
            new ServiceItem { ServiceProviderId = providers[0].Id, Name = "Phòng Family", ItemType = "Room", Capacity = 4, Price = 2200000, Description = "Hai phòng ngủ.", IsAvailable = true },
            new ServiceItem { ServiceProviderId = providers[1].Id, Name = "Phòng Superior", ItemType = "Room", Capacity = 2, Price = 900000, Description = "Gần phố cổ.", IsAvailable = true },
            new ServiceItem { ServiceProviderId = providers[2].Id, Name = "Bàn 4 người", ItemType = "Table", Capacity = 4, Price = 0, Description = "Bàn trong nhà.", IsAvailable = true },
            new ServiceItem { ServiceProviderId = providers[3].Id, Name = "Villa hướng vịnh", ItemType = "Room", Capacity = 3, Price = 3500000, Description = "Villa riêng.", IsAvailable = true }
        );

        db.Schedules.AddRange(
            new Schedule { TransportRouteId = routes[0].Id, VehicleId = vehicles[0].Id, DepartureTime = new DateTime(2026, 11, 16, 8, 0, 0), ArrivalTime = new DateTime(2026, 11, 16, 9, 0, 0), Price = 150000 },
            new Schedule { TransportRouteId = routes[1].Id, VehicleId = vehicles[0].Id, DepartureTime = new DateTime(2026, 11, 16, 7, 30, 0), ArrivalTime = new DateTime(2026, 11, 16, 10, 30, 0), Price = 250000 },
            new Schedule { TransportRouteId = routes[2].Id, VehicleId = vehicles[1].Id, DepartureTime = new DateTime(2026, 11, 16, 18, 0, 0), ArrivalTime = new DateTime(2026, 11, 17, 6, 0, 0), Price = 450000 },
            new Schedule { TransportRouteId = routes[1].Id, VehicleId = vehicles[2].Id, DepartureTime = new DateTime(2026, 11, 17, 9, 0, 0), ArrivalTime = new DateTime(2026, 11, 17, 12, 0, 0), Price = 180000 }
        );

        db.Contacts.AddRange(
            new Contact { FullName = "Trần Thu Hà", Email = "ha.tran@example.com", Phone = "0912000001", Subject = "Hỏi tour Hội An", Message = "Cho mình xin lịch tour cuối tuần.", Status = "New", CreatedAt = now },
            new Contact { FullName = "Lê Quốc Bảo", Email = "bao.le@example.com", Phone = "0912000002", Subject = "Đặt xe nhóm", Message = "Nhóm 12 người đi Huế.", Status = "Processed", CreatedAt = now.AddDays(-1), ProcessedAt = now, ProcessedByUser = admin }
        );

        db.Feedbacks.AddRange(
            new Feedback { User = user, FullName = user.FullName, Email = user.Email, TargetType = "TouristSpot", TargetId = spots[0].Id, Rating = 5, Content = "Cầu Vàng rất đẹp.", Status = "Approved", CreatedAt = now.AddDays(-2), ReviewedAt = now.AddDays(-1), ReviewedByUser = admin },
            new Feedback { User = user, FullName = user.FullName, Email = user.Email, TargetType = "Tour", TargetId = tours[1].Id, Rating = 4, Content = "Hướng dẫn viên nhiệt tình.", Status = "Approved", CreatedAt = now.AddDays(-1), ReviewedAt = now, ReviewedByUser = admin },
            new Feedback { FullName = "Phạm Lan", Email = "lan.pham@example.com", TargetType = "General", Rating = 3, Content = "Web tải hơi chậm.", Status = "Pending", CreatedAt = now },
            new Feedback { FullName = "Ẩn danh", Email = "an@example.com", TargetType = "Service", TargetId = providers[2].Id, Rating = 2, Content = "Nội dung không phù hợp.", Status = "Hidden", CreatedAt = now.AddDays(-3), ReviewedAt = now.AddDays(-2), ReviewedByUser = admin }
        );

        db.Notifications.Add(new Notification
        {
            User = user,
            Title = "Chào mừng đến Karnel",
            Message = "Tài khoản của bạn đã sẵn sàng.",
            Type = "System",
            IsRead = false,
            CreatedAt = now
        });

        db.EmailLogs.Add(new EmailLog
        {
            ToEmail = user.Email,
            Subject = "Chào mừng",
            Body = "Cảm ơn bạn đã đăng ký Karnel Travel Guide.",
            Status = "Sent",
            CreatedAt = now
        });

        db.SystemLogs.Add(new SystemLog
        {
            User = admin,
            Action = "Seed",
            EntityName = "Database",
            Detail = "Khởi tạo dữ liệu mẫu.",
            IpAddress = "127.0.0.1",
            CreatedAt = now
        });

        db.SaveChanges();

        var booking = new Booking
        {
            Code = "BK20261108001",
            UserId = user.Id,
            BookingType = "Tour",
            Status = "Confirmed",
            TotalAmount = 1780000,
            ContactName = user.FullName,
            ContactPhone = user.Phone!,
            ContactEmail = user.Email,
            Note = "Đi 2 người.",
            CreatedAt = now,
            Details =
            {
                new BookingDetail
                {
                    ItemType = "TourPackage",
                    ItemId = tours[1].Id,
                    ItemName = tours[1].Name,
                    Quantity = 2,
                    UnitPrice = 890000,
                    ServiceDate = new DateTime(2026, 11, 16)
                }
            }
        };
        db.Bookings.Add(booking);
        db.SaveChanges();
    }

    public static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}
