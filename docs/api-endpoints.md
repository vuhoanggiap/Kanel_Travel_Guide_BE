# Karnel Travel Guide — Tài liệu API (hạn 09/11)

Base URL khi chạy local: `http://localhost:5098`

OpenAPI: `http://localhost:5098/openapi/v1.json`

Múi giờ trong dữ liệu mẫu là UTC. Tiền dùng VND, kiểu số.

API nghiệp vụ (điểm du lịch, tour, đặt dịch vụ, tìm kiếm) do **Đoàn Anh Kiệt** viết. API tài khoản, liên hệ, phản hồi, thông báo, backup do **Vũ Hoàng Giáp** viết. Tài liệu này chốt JSON để frontend làm trước.

## Chạy database

Cần SQL Server LocalDB.

```powershell
dotnet run --project src/KarnelTravelGuide.Api --launch-profile http
```

Lần chạy đầu tự tạo database `KarnelTravelGuide`, chạy migration và đổ dữ liệu mẫu.

Tài khoản mẫu:

| Vai trò | Email | Mật khẩu |
|---|---|---|
| Admin | admin@karnel.local | Admin@123 |
| User | user@karnel.local | User@123 |

CORS đang mở cho `http://localhost:5173`, `http://localhost:3000`, `http://localhost:4200`.

Sau khi đăng nhập, các API cần quyền gửi header:

```http
Authorization: Bearer {token}
```

`token` có ở hạn 13/11. Trước đó frontend có thể gắn giao diện với JSON mẫu bên dưới.

## Giá trị dùng chung

- `role`: `Admin`, `User`
- Liên hệ `status`: `New`, `Processed`
- Phản hồi `status`: `Pending`, `Approved`, `Hidden`
- Phản hồi `targetType`: `General`, `TouristSpot`, `Tour`, `Service`
- Thông báo `type`: `System`, `Booking`, `Promotion`
- Dịch vụ `type`: `Hotel`, `Restaurant`, `Resort`
- Hạng mục `itemType`: `Room`, `Table`
- Xe `vehicleType`: `Bus`, `Car`, `Train`
- Đơn `bookingType`: `Tour`, `Hotel`, `Restaurant`, `Resort`, `Transport`
- Đơn `status`: `Pending`, `Confirmed`, `Cancelled`, `Completed`
- Dòng đơn `itemType`: `TourPackage`, `ServiceItem`, `Schedule`

Lỗi dùng chung:

```json
{
  "message": "Mô tả lỗi"
}
```

## Kiểm tra server

`GET /api/health`

```json
{
  "status": "ok",
  "database": "connected"
}
```

## Tài khoản — Giáp (code hạn 13/11 và 15/11)

### Đăng ký

`POST /api/auth/register`

```json
{
  "fullName": "Nguyễn Minh An",
  "email": "an.nguyen@example.com",
  "password": "User@123",
  "phone": "0901000002"
}
```

Trả về `201`:

```json
{
  "id": 3,
  "fullName": "Nguyễn Minh An",
  "email": "an.nguyen@example.com",
  "phone": "0901000002",
  "role": "User"
}
```

### Đăng nhập

`POST /api/auth/login`

```json
{
  "email": "admin@karnel.local",
  "password": "Admin@123"
}
```

```json
{
  "token": "jwt-se-co-o-han-13-11",
  "user": {
    "id": 1,
    "fullName": "Quản trị Karnel",
    "email": "admin@karnel.local",
    "role": "Admin"
  }
}
```

### Đăng xuất

`POST /api/auth/logout` — cần đăng nhập. Trả về `204`.

### Đổi mật khẩu

`POST /api/auth/change-password`

```json
{
  "currentPassword": "User@123",
  "newPassword": "User@456"
}
```

### Quên mật khẩu và đặt lại

`POST /api/auth/forgot-password`

```json
{
  "email": "user@karnel.local"
}
```

`POST /api/auth/reset-password`

```json
{
  "email": "user@karnel.local",
  "token": "ma-trong-email",
  "newPassword": "User@456"
}
```

Hai API này đi cùng gửi email, hạn 17/11.

### Hồ sơ

`GET /api/account/profile`

```json
{
  "id": 2,
  "fullName": "Nguyễn Minh An",
  "email": "user@karnel.local",
  "phone": "0901000002",
  "address": "Sơn Trà, Đà Nẵng",
  "avatarUrl": null,
  "role": "User"
}
```

`PUT /api/account/profile`

```json
{
  "fullName": "Nguyễn Minh An",
  "phone": "0901000009",
  "address": "Hải Châu, Đà Nẵng",
  "avatarUrl": "/images/avatar.jpg"
}
```

### Quản lý tài khoản (Admin)

`GET /api/account/users`

```json
[
  {
    "id": 2,
    "fullName": "Nguyễn Minh An",
    "email": "user@karnel.local",
    "phone": "0901000002",
    "role": "User",
    "isActive": true
  }
]
```

`PUT /api/account/users/{id}`

```json
{
  "role": "User",
  "isActive": false
}
```

## Liên hệ và phản hồi — Giáp (code hạn 11/11 và 15/11)

### Gửi liên hệ

`POST /api/contacts`

```json
{
  "fullName": "Trần Thu Hà",
  "email": "ha.tran@example.com",
  "phone": "0912000001",
  "subject": "Hỏi tour Hội An",
  "message": "Cho mình xin lịch tour cuối tuần."
}
```

```json
{
  "id": 1,
  "status": "New",
  "createdAt": "2026-11-08T08:00:00Z"
}
```

### Danh sách liên hệ (Admin)

`GET /api/contacts`

```json
[
  {
    "id": 1,
    "fullName": "Trần Thu Hà",
    "email": "ha.tran@example.com",
    "phone": "0912000001",
    "subject": "Hỏi tour Hội An",
    "message": "Cho mình xin lịch tour cuối tuần.",
    "status": "New",
    "createdAt": "2026-11-08T08:00:00Z",
    "processedAt": null
  }
]
```

### Đánh dấu đã xử lý (Admin)

`PUT /api/contacts/{id}/processed` — trả về `204`.

### Gửi phản hồi

`POST /api/feedbacks`

```json
{
  "fullName": "Nguyễn Minh An",
  "email": "user@karnel.local",
  "targetType": "TouristSpot",
  "targetId": 1,
  "rating": 5,
  "content": "Cầu Vàng rất đẹp."
}
```

```json
{
  "id": 3,
  "status": "Pending"
}
```

### Phản hồi đã duyệt (trang công khai)

`GET /api/feedbacks?targetType=TouristSpot&targetId=1`

```json
[
  {
    "id": 1,
    "fullName": "Nguyễn Minh An",
    "targetType": "TouristSpot",
    "targetId": 1,
    "rating": 5,
    "content": "Cầu Vàng rất đẹp.",
    "createdAt": "2026-11-06T08:00:00Z"
  }
]
```

Chỉ trả bản ghi `Approved`.

### Quản lý phản hồi (Admin)

`GET /api/feedbacks/manage`

`PUT /api/feedbacks/{id}/status`

```json
{
  "status": "Approved"
}
```

`status` nhận `Approved` hoặc `Hidden`.

## Thông báo, nhật ký, backup — Giáp (code hạn 17/11)

`GET /api/notifications`

```json
[
  {
    "id": 1,
    "title": "Chào mừng đến Karnel",
    "message": "Tài khoản của bạn đã sẵn sàng.",
    "type": "System",
    "isRead": false,
    "createdAt": "2026-11-08T08:00:00Z"
  }
]
```

`PUT /api/notifications/{id}/read` — trả về `204`.

`GET /api/system/logs` (Admin)

```json
[
  {
    "id": 1,
    "action": "Seed",
    "entityName": "Database",
    "entityId": null,
    "detail": "Khởi tạo dữ liệu mẫu.",
    "createdAt": "2026-11-08T08:00:00Z"
  }
]
```

`POST /api/system/backup` và `POST /api/system/restore` (Admin). Restore nhận file backup. Chi tiết file chốt khi làm CN24.

## Trang chủ, điểm du lịch, loại hình — Kiệt

`GET /api/home`

```json
{
  "spots": [
    {
      "id": 1,
      "name": "Bà Nà Hills",
      "locationName": "Đà Nẵng",
      "categoryName": "Núi",
      "imageUrl": "/images/ba-na.jpg",
      "ticketPrice": 900000
    }
  ],
  "tours": [
    {
      "id": 1,
      "name": "Đà Nẵng 2 ngày",
      "price": 2500000,
      "durationDays": 2,
      "imageUrl": "/images/tour-danang.jpg"
    }
  ],
  "promotions": [
    {
      "id": 1,
      "title": "Giảm tour Đà Nẵng",
      "code": "DANANG10",
      "discountPercent": 10
    }
  ]
}
```

`GET /api/locations`

```json
[
  { "id": 1, "name": "Đà Nẵng", "city": "Đà Nẵng", "province": "Đà Nẵng" }
]
```

`GET /api/category-types`

```json
[
  { "id": 1, "name": "Biển", "description": "Điểm đến ven biển." }
]
```

`GET /api/tourist-spots?locationId=1&categoryTypeId=2&q=Hội`

```json
[
  {
    "id": 3,
    "name": "Phố cổ Hội An",
    "locationId": 2,
    "locationName": "Hội An",
    "categoryTypeId": 2,
    "categoryName": "Di tích",
    "address": "Minh An, Hội An",
    "ticketPrice": 120000,
    "imageUrl": "/images/hoi-an.jpg"
  }
]
```

`GET /api/tourist-spots/{id}`

```json
{
  "id": 1,
  "name": "Bà Nà Hills",
  "locationId": 1,
  "locationName": "Đà Nẵng",
  "categoryTypeId": 3,
  "categoryName": "Núi",
  "description": "Khu du lịch trên núi với cầu Vàng.",
  "address": "Hòa Ninh, Hòa Vang, Đà Nẵng",
  "openTime": "07:00-22:00",
  "ticketPrice": 900000,
  "imageUrl": "/images/ba-na.jpg",
  "isActive": true
}
```

`POST /api/tourist-spots`, `PUT /api/tourist-spots/{id}`, `DELETE /api/tourist-spots/{id}` (Admin). Body tạo/sửa giống JSON chi tiết, bỏ `id` và các trường `*Name`.

## Tour và ưu đãi — Kiệt

`GET /api/tours` và `GET /api/tours/{id}`

```json
{
  "id": 1,
  "name": "Đà Nẵng 2 ngày",
  "touristSpotId": 1,
  "touristSpotName": "Bà Nà Hills",
  "description": "Bà Nà Hills và biển Mỹ Khê.",
  "durationDays": 2,
  "price": 2500000,
  "maxGuests": 20,
  "imageUrl": "/images/tour-danang.jpg",
  "isActive": true
}
```

`POST /api/tours`, `PUT /api/tours/{id}`, `DELETE /api/tours/{id}` (Admin).

`GET /api/promotions`

```json
[
  {
    "id": 1,
    "title": "Giảm tour Đà Nẵng",
    "code": "DANANG10",
    "description": "Giảm 10% tour Đà Nẵng 2 ngày.",
    "discountPercent": 10,
    "startDate": "2026-11-08T00:00:00Z",
    "endDate": "2027-01-08T00:00:00Z",
    "tourPackageId": 1,
    "isActive": true
  }
]
```

`POST /api/promotions`, `PUT /api/promotions/{id}`, `DELETE /api/promotions/{id}` (Admin).

## Lưu trú và ẩm thực — Kiệt

`GET /api/services?type=Hotel`

`type` nhận `Hotel`, `Restaurant`, `Resort`. Bỏ trống thì trả cả ba.

```json
[
  {
    "id": 1,
    "name": "Karnel Beach Hotel",
    "type": "Hotel",
    "locationId": 1,
    "locationName": "Đà Nẵng",
    "address": "Võ Nguyên Giáp, Đà Nẵng",
    "phone": "02361111001",
    "starRating": 4,
    "imageUrl": "/images/hotel-beach.jpg"
  }
]
```

`GET /api/services/{id}` kèm phòng hoặc bàn:

```json
{
  "id": 1,
  "name": "Karnel Beach Hotel",
  "type": "Hotel",
  "locationId": 1,
  "locationName": "Đà Nẵng",
  "address": "Võ Nguyên Giáp, Đà Nẵng",
  "description": "Khách sạn view biển.",
  "phone": "02361111001",
  "starRating": 4,
  "imageUrl": "/images/hotel-beach.jpg",
  "items": [
    {
      "id": 1,
      "name": "Phòng Deluxe",
      "itemType": "Room",
      "capacity": 2,
      "price": 1500000,
      "description": "View biển, 1 giường lớn.",
      "isAvailable": true
    }
  ]
}
```

`POST /api/services`, `PUT /api/services/{id}`, `DELETE /api/services/{id}` (Admin).

`POST /api/services/{id}/items`, `PUT /api/services/{serviceId}/items/{itemId}`, `DELETE /api/services/{serviceId}/items/{itemId}` (Admin).

## Tìm kiếm — Kiệt

`GET /api/search?q=Hội An`

```json
{
  "spots": [],
  "tours": [],
  "services": []
}
```

Mỗi mảng dùng JSON rút gọn của danh sách tương ứng.

`GET /api/search/advanced?q=biển&locationId=1&categoryTypeId=1&type=Hotel&minPrice=0&maxPrice=3000000&sort=price_asc`

`sort` nhận `price_asc`, `price_desc`, `name_asc`.

## Vận chuyển — Kiệt

`GET /api/routes`

```json
[
  {
    "id": 1,
    "name": "Đà Nẵng - Hội An",
    "fromPlace": "Đà Nẵng",
    "toPlace": "Hội An",
    "distanceKm": 30
  }
]
```

`GET /api/routes/{id}` kèm lịch chạy:

```json
{
  "id": 1,
  "name": "Đà Nẵng - Hội An",
  "fromPlace": "Đà Nẵng",
  "toPlace": "Hội An",
  "distanceKm": 30,
  "description": "Tuyến xe dọc biển.",
  "schedules": [
    {
      "id": 1,
      "vehicleId": 1,
      "vehicleName": "Xe 16 chỗ Karnel",
      "vehicleType": "Car",
      "departureTime": "2026-11-16T08:00:00",
      "arrivalTime": "2026-11-16T09:00:00",
      "price": 150000
    }
  ]
}
```

`POST /api/routes`, `PUT /api/routes/{id}`, `DELETE /api/routes/{id}` (Admin).

`GET /api/vehicles`

`POST /api/vehicles`, `PUT /api/vehicles/{id}`, `DELETE /api/vehicles/{id}` (Admin).

```json
{
  "name": "Xe 16 chỗ Karnel",
  "vehicleType": "Car",
  "plateNumber": "43A-10001",
  "seatCount": 16,
  "isActive": true
}
```

`GET /api/schedules?routeId=1`

`POST /api/schedules`, `PUT /api/schedules/{id}`, `DELETE /api/schedules/{id}` (Admin).

```json
{
  "transportRouteId": 1,
  "vehicleId": 1,
  "departureTime": "2026-11-16T08:00:00",
  "arrivalTime": "2026-11-16T09:00:00",
  "price": 150000
}
```

## Đặt dịch vụ — Kiệt

`POST /api/bookings`

```json
{
  "bookingType": "Tour",
  "contactName": "Nguyễn Minh An",
  "contactPhone": "0901000002",
  "contactEmail": "user@karnel.local",
  "note": "Đi 2 người.",
  "promotionCode": "HOIAN15",
  "details": [
    {
      "itemType": "TourPackage",
      "itemId": 2,
      "quantity": 2,
      "serviceDate": "2026-11-16"
    }
  ]
}
```

```json
{
  "id": 1,
  "code": "BK20261108001",
  "bookingType": "Tour",
  "status": "Pending",
  "totalAmount": 1513000,
  "createdAt": "2026-11-08T08:00:00Z"
}
```

`GET /api/bookings` — đơn của user đang đăng nhập.

`GET /api/bookings/{id}`

```json
{
  "id": 1,
  "code": "BK20261108001",
  "bookingType": "Tour",
  "status": "Confirmed",
  "totalAmount": 1780000,
  "contactName": "Nguyễn Minh An",
  "contactPhone": "0901000002",
  "contactEmail": "user@karnel.local",
  "note": "Đi 2 người.",
  "createdAt": "2026-11-08T08:00:00Z",
  "details": [
    {
      "itemType": "TourPackage",
      "itemId": 2,
      "itemName": "Hội An về đêm",
      "quantity": 2,
      "unitPrice": 890000,
      "serviceDate": "2026-11-16T00:00:00"
    }
  ]
}
```

`PUT /api/bookings/{id}/cancel`

```json
{
  "reason": "Đổi ngày đi"
}
```

`GET /api/bookings/manage?status=Pending` (Admin).

`PUT /api/bookings/{id}/status` (Admin)

```json
{
  "status": "Confirmed"
}
```

Khi tạo hoặc hủy đơn, API gọi sang phần thông báo và nhật ký của Giáp.
