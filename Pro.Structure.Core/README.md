# Pro.Structure.Core

The core domain layer of the Pro.Structure application, implementing clean architecture principles. This project contains all business entities, interfaces, and core business logic, completely independent of infrastructure or presentation concerns.

## Project Structure

### Entities
- `BaseEntity.cs`: Abstract base class providing common entity properties
- `Project.cs`: Core project entity with business rules and validation
- `Customer.cs`: Customer entity definition
- `ProjectManager.cs`: Project manager entity and related business rules
- `Status.cs`: Project status definitions

### Interfaces
- `IBaseRepository.cs`: Generic repository interface for data access
- `IProjectService.cs`: Project-related business operations
- `ICustomerService.cs`: Customer management operations
- `IProjectManagerService.cs`: Project manager related operations

### Models
- `ServiceResponse.cs`: Generic response wrapper for service operations
- DTOs for data transfer between layers
- Value objects for encapsulating business rules

### Factories
- Entity to DTO mapping logic
- Business object creation patterns

## Key Features

- Clean separation of concerns
- Domain-driven design principles
- Rich domain models with business logic
- Interface-based design for dependency inversion
- Immutable value objects where appropriate
- Generic base classes for common functionality

## Dependencies

The Core project has minimal dependencies, maintaining its independence from infrastructure concerns:

- .NET 9.0
- Basic BCL libraries only

## Usage

This project serves as the foundation for the entire application. Other projects should:

1. Reference this project
2. Implement its interfaces
3. Use its models for data transfer
4. Follow its business rules and constraints

## Design Principles

- **Independence**: No dependencies on UI or infrastructure
- **Encapsulation**: Business rules within domain entities
- **Rich Domain Model**: Business logic in domain objects
- **Immutability**: Where appropriate for thread safety
- **SOLID Principles**: Particularly SRP and DIP

## Testing

The Core project is designed to be easily testable:

- Pure C# business logic
- No external dependencies
- Interface-based design
- Immutable objects where possible 