# CampusComplaints.Web

Campus complaints tracking system built with ASP.NET Core MVC and Entity Framework Core. It provides a simple staff-facing UI for logging, updating, and triaging student issues, plus a small REST API that can be consumed by a separate frontend (CORS enabled for `http://localhost:3000` by default).

## Features

- Complaint intake form with validation, category selection, and reporter contact fields.
- CRUD dashboard for admins/staff to review, edit, delete, and change complaint statuses.
- Status history fields (`CreatedAtUtc`, `UpdatedAtUtc`) to track progress.
- API surface under `/api/complaints` for integrating a React or mobile frontend.
- SQLite persistence (`app.db`) for lightweight local development.

## app overview
<img width="1919" height="880" alt="Screenshot 2025-10-30 235519" src="https://github.com/user-attachments/assets/dad82cef-7825-4878-8e85-87c921784ddb" />
<img width="1912" height="880" alt="Screenshot 2025-10-30 235647" src="https://github.com/user-attachments/assets/7378a14d-8ed9-4764-848b-e522e47ec492" />
<img width="1660" height="634" alt="Screenshot 2025-10-31 000302" src="https://github.com/user-attachments/assets/a4a3a1a1-1994-4d1b-a7b7-fb19cc489ff8" />
<img width="1913" height="796" alt="Screenshot 2025-10-31 001708" src="https://github.com/user-attachments/assets/b34d3031-7304-4cb8-8e62-41614ac2c97d" />
<img width="885" height="527" alt="Screenshot 2025-10-31 001733" src="https://github.com/user-attachments/assets/c5c7aff1-4570-4a7d-b209-2e69b7224b62" />

 ## Tech Stack

- .NET 8 / ASP.NET Core MVC
- Entity Framework Core with SQLite provider
- Razor Views with Bootstrap 5 and unobtrusive validation

## Getting Started

1. **Prerequisites**
   - [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) (includes the `dotnet` CLI)
   - Optional: `dotnet-ef` CLI (`dotnet tool install --global dotnet-ef`) for manual migration commands

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations** (creates/updates `app.db`)
   ```bash
   dotnet ef database update --project CampusComplaints.Web.csproj
   ```

4. **Run the application**
   ```bash
   dotnet run --project CampusComplaints.Web.csproj
   ```
   By default the app responds on `https://localhost:5001` (HTTPS) and `http://localhost:5000` (HTTP).

## Key Configuration

- `appsettings.json` contains the `DefaultConnection` pointing at the local `app.db` file. Change this to point at SQL Server/Postgres for production and update `UseSqlite` accordingly.
- CORS policy named `Frontend` allows requests from `http://localhost:3000`. Update `Program.cs` to add more origins when deploying.

## API Quick Reference

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/complaints` | List complaints (newest first) |
| `GET` | `/api/complaints/{id}` | Fetch a specific complaint |
| `POST` | `/api/complaints` | Create a complaint (JSON body matches `Complaint` model) |
| `PUT` | `/api/complaints/{id}` | Replace complaint details |
| `PATCH` | `/api/complaints/{id}/status?status=Resolved` | Update only the status |
| `DELETE` | `/api/complaints/{id}` | Remove a complaint |

All endpoints return standard HTTP status codes and rely on EF Core validation attributes defined in `Models/Complaint.cs`.

## Complaint Model

```
Id, Title (required, 120 chars), Description (required, 4000 chars),
Category (required, 100 chars), ReporterName, ReporterEmail (validated),
Status (Open/InProgress/Resolved/Closed), CreatedAtUtc, UpdatedAtUtc.
```

## Development Tips

- Use `dotnet watch run` for hot reload during UI development.
- Create additional migrations with `dotnet ef migrations add <Name> --project CampusComplaints.Web.csproj`.
- When pairing with a JavaScript frontend, point it at the `/api/complaints` routes and ensure the origin matches the configured CORS policy.

## License

No license has been specified yet. Add one before distributing this project.





