# Pro.Structure.Infrastructure

Implementation layer handling data access, persistence, and external service integration for the Pro.Structure application. This project implements the interfaces defined in Pro.Structure.Core and provides concrete implementations of repositories and services.

## Project Structure

### Data Access
- `ApplicationDbContext.cs`: Entity Framework Core database context
- Entity configurations and relationships
- Migration management
- SQLite database implementation

### Repositories
- `BaseRepository.cs`: Generic repository implementation
- `ProjectRepository.cs`: Project-specific data access
- `CustomerRepository.cs`: Customer data operations
- `ProjectManagerRepository.cs`: Project manager persistence
- `StatusRepository.cs`: Status management

### Services
- `ProjectService.cs`: Business logic for project management
- `CustomerService.cs`: Customer-related operations
- `ProjectManagerService.cs`: Project manager operations
- `StatusService.cs`: Status tracking implementation

### Migrations
- Database schema evolution
- Data seeding
- Version control for database structure

## Features

- Entity Framework Core with SQLite
- Generic repository pattern implementation
- Asynchronous operations
- Transaction management
- Data validation and business rules
- Automatic audit trails (Created/Modified dates)

## Configuration

### Database Connection
The default configuration uses SQLite with the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Pro.Structure.db"
  }
}
```

### Entity Framework Migrations
To manage database updates:
```bash
dotnet ef migrations add [MigrationName]
dotnet ef database update
```

## Dependencies

- Pro.Structure.Core
- Entity Framework Core
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.DependencyInjection

## Service Registration

Services are registered in `DependencyInjection.cs`:
```csharp
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<IProjectService, ProjectService>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<IProjectManagerService, ProjectManagerService>();
```

## Data Access Patterns

- Repository Pattern for data access abstraction
- Unit of Work pattern for transaction management
- Eager loading for related entities
- Async/await for all database operations

## Security

- SQL injection prevention through EF Core
- Data validation before persistence
- Audit trail for all modifications
- Proper exception handling

## Performance

- Efficient query generation
- Proper use of Include() for related data
- Async operations for better scalability
- Connection pooling

## Purpose
The Infrastructure project handles all external concerns and provides concrete implementations of the interfaces defined in the Core project. It manages data persistence, external service integration, and other infrastructure-related tasks. 