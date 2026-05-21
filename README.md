# Task Management

An ASP.NET Core 8 MVC + PostgreSQL task management application.

## Technologies Used in the Project

- ASP.NET Core 8 MVC
- Entity Framework Core 8 + Npgsql
- AutoMapper 13
- PostgreSQL 16
- Bootstrap 5

## Notes

**Status is not stored in the DB.** `TaskItem.Status` is a computed property, calculated at runtime on each request:

```text
IsCompleted = true          ? Done
Deadline has passed         ? Overdue
Deadline is within 24 hours ? Urgent
Other cases                 ? Active
```

## Running with Docker

```bash
docker compose up --build
```

The application opens at `http://localhost:5000`.

On the first run, `EnsureCreated()` automatically creates the database tables.

## Running Locally (Without Docker)

Change the PostgreSQL password in the `appsettings.Development.json` file:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=task_management_db;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Then run:

```bash
dotnet run
```