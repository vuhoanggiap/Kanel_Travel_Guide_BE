# Karnel Travel Guide — Backend

Repo này chỉ chứa **backend** (ASP.NET Core Web API). Chưa có giao diện web.

Frontend do Ánh Huyền, Tiến Dũng và Công Tiến làm ở repo riêng. Bên đó gọi API theo `docs/api-endpoints.md`.

## Trong repo này

- `DbContext`, migration và dữ liệu mẫu cho 18 bảng
- Tài liệu API: danh sách endpoint và JSON mẫu
- File thiết kế CSDL: `docs/Thiet_Ke_CSDL_Karnel_Travel_Guide.xlsx`
- Một endpoint kiểm tra: `GET /api/health`

Chưa có API đăng nhập, liên hệ, điểm du lịch hay đặt dịch vụ. Những API đó làm ở các hạn sau.

## Chạy

Cần .NET 10 và SQL Server LocalDB.

```powershell
dotnet run --project src/KarnelTravelGuide.Api --launch-profile http
```

API chạy tại `http://localhost:5098`. Lần chạy đầu tạo database `KarnelTravelGuide` và đổ dữ liệu mẫu.

| Vai trò | Username | Mật khẩu |
|---|---|---|
| Admin | admin | Admin@123 |
| User | minhan | User@123 |
