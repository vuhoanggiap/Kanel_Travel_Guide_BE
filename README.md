# Karnel Travel Guide — Backend

This repository contains the backend only (ASP.NET Core Web API). It does not contain the web interface.

The frontend is a separate repository. It should follow the JSON samples in `docs/api-endpoints.md`.

## Included

- `DbContext`, the migration, and sample data for 18 tables
- API documentation: endpoint list and JSON samples
- Database design file: `docs/Thiet_Ke_CSDL_Karnel_Travel_Guide.xlsx`
- One check endpoint: `GET /api/health`

Login, contact, tourist-spot, and booking APIs are not implemented yet.

## Run

Requires .NET 10 and SQL Server LocalDB.

```powershell
dotnet run --project src/KarnelTravelGuide.Api --launch-profile http
```

The API runs at `http://localhost:5098`. The first run creates the `KarnelTravelGuide` database and loads the sample data.

| Role | Username | Password |
|---|---|---|
| Admin | admin | Admin@123 |
| User | minhan | User@123 |
