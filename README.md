# sz-core-template

## Overview
A modern web API template built on .NET Core, customized for rapid MVP development and scalable ERP systems.
This template is configured to use PostgreSQL as its database engine, supporting database-first workflows and robust data management.
With database-first support, organized structure, standardized responses, and flexible parameterized requests, this project helps you build robust business applications quickly and efficiently.
Perfect for teams who want to deliver reliable, secure, and high-performance solutions with minimal setup.

## Features
- **Layered Architecture**: Clear separation of concerns (Controllers, Services, Repositories, Data, Entities, Models, Helpers).
- **Repository Pattern**: Abstracts data access logic for maintainability and testability.
- **Service Layer Pattern**: Encapsulates business logic, keeping controllers thin.
- **Dependency Injection**: Uses interfaces and constructor injection for loose coupling.
- **DTOs and AutoMapper**: Data Transfer Objects and mapping profiles for clean data exchange.
- **Exception Handling**: Custom exceptions for robust error management.
- **Extension Methods**: Helpers for queryable and API extensions.
- **Entity Framework Core**: Modern ORM for data access and migrations.
- **Configuration and Logging**: Centralized configuration and NLog for logging.
- **Logging System**: Integrated logging for monitoring and troubleshooting.
- **Authentication**: JWT authentication for secure APIs.
- **Paging, Filtering, Ordering**: Generic base models and query helpers for advanced querying.

## Database Scaffolding
This project supports database-first development using EF Core scaffolding. To generate entities and DbContext from your database, run:

```powershell
dotnet ef dbcontext scaffold "Name=ConnectionStrings:DefaultConnection" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Entities --context-dir Data --context AppDbContext -f -d --project "API"
```

- `--output-dir Entities`: Puts generated entity classes in the `Entities` folder.
- `--context-dir Data`: Puts the DbContext in the `Data` folder.
- `--context AppDbContext`: Names the DbContext.
- `-f`: Overwrites existing files.
- `-d`: Uses database names directly.
- `--project "API"`: Targets the API project.

## Project Structure
```
API/
  Controllers/        # API endpoints
  Data/               # DbContext and EF Core setup
  Entities/           # Data models
  Extensions/         # Extension methods
  Helpers/            # Utility classes (e.g., AutoMapper profiles)
  Interfaces/         # Service and repository interfaces
  Models/             # DTOs, parameters, base models, exceptions
  Repositories/       # Data access logic
  Services/           # Business logic
```

## Design Patterns Used
- **Repository Pattern**: Encapsulates data access logic.
- **Service Layer Pattern**: Encapsulates business logic.
- **Dependency Injection**: Promotes loose coupling.
- **DTO Pattern**: Separates internal models from exposed data.

## How to Run
1. Restore dependencies:
	```powershell
	dotnet restore
	```
2. Update database (if using migrations):
	```powershell
	dotnet ef database update
	```
3. Run the API:
	```powershell
	dotnet run
	```

## Customization
- Add new entities to `Entities/` and update `DbContext`.
- Create new repositories/services by following the existing patterns.
- Add new endpoints in `Controllers/`.

## License
MIT
