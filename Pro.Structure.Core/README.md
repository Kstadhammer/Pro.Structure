# Pro.Structure.Core

The core domain layer of the Mattin-Lassei Group AB project management system, implementing clean architecture principles. This project contains all business entities, interfaces, and core business logic, completely independent of infrastructure or presentation concerns.

## Project Structure

### Entities
- `BaseEntity.cs`: Abstract base class providing common entity properties (Id, Created, Modified)
- `Project.cs`: Core project entity with business rules and validation
- `Customer.cs`: Customer entity with contact information and project relationships
- `ProjectManager.cs`: Project manager entity with workload tracking
- `Status.cs`: Dynamic status entity for project state management

### Interfaces
#### Repositories
- `IBaseRepository.cs`: Generic repository interface for data access
- `IProjectRepository.cs`: Project-specific data operations
- `ICustomerRepository.cs`: Customer data management
- `IProjectManagerRepository.cs`: Project manager data operations
- `IStatusRepository.cs`: Status management operations

#### Services
- `IBaseService.cs`: Generic service interface with response handling
- `IProjectService.cs`: Project business operations
- `ICustomerService.cs`: Customer management operations
- `IProjectManagerService.cs`: Project manager operations
- `IStatusService.cs`: Status management operations

### Models
- `ServiceResponse.cs`: Generic response wrapper for consistent error handling
- `ProjectModel.cs`: Project data transfer object
- `CustomerModel.cs`: Customer data transfer object
- `ProjectManagerModel.cs`: Project manager data transfer object
- `StatusModel.cs`: Status data transfer object

### Factories
- `IFactory.cs`: Generic factory interface for entity/model conversion
- Consistent mapping between entities and view models
- Support for complex object creation

## Key Features

### Domain Logic
- Rich domain models with business rules
- Validation logic within entities
- Complex relationship management
- Workload tracking calculations

### Service Layer
- Consistent error handling through ServiceResponse
- Business rule enforcement
- Transaction management
- Event handling preparation

### Repository Abstractions
- Generic CRUD operations
- Specialized query methods
- Async/await pattern support
- Flexible query specifications

## Design Principles

### Independence
- No dependencies on UI or infrastructure
- Pure C# business logic
- Framework-agnostic design
- Testability focus

### Encapsulation
- Business rules within domain entities
- Data validation at the core
- Protected state management
- Immutable where appropriate

### SOLID Principles
- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

## Best Practices

### Error Handling
- Use of ServiceResponse pattern
- Consistent error messages
- Proper null handling
- Validation results

### Async Operations
- Async/await throughout
- Cancellation support
- Task-based operations
- Performance considerations

### Validation
- Entity-level validation
- Business rule enforcement
- Cross-entity validation
- State transition validation

## Testing

The Core project is designed for comprehensive testing:

### Unit Testing
- Pure C# business logic
- No external dependencies
- Interface-based design
- Mockable components

### Integration Testing
- Service layer testing
- Cross-entity operations
- Business rule validation
- State transition testing

## Dependencies

The Core project maintains minimal dependencies:

- .NET 9.0
- Basic BCL libraries
- No external packages
- No infrastructure dependencies

## Usage Guidelines

### Entity Creation
```csharp
public class NewEntity : BaseEntity
{
    // Include required properties
    public string RequiredProperty { get; set; } = null!;
    
    // Add navigation properties
    public int RelatedEntityId { get; set; }
    public RelatedEntity RelatedEntity { get; set; } = null!;
}
```

### Interface Implementation
```csharp
public interface INewEntityRepository : IBaseRepository<NewEntity>
{
    Task<NewEntity?> GetByUniquePropertyAsync(string property);
    Task<bool> ExistsByUniquePropertyAsync(string property);
}
```

### Service Pattern
```csharp
public interface INewEntityService : IBaseService<NewEntity>
{
    Task<ServiceResponse<NewEntityModel>> GetByUniquePropertyAsync(string property);
    Task<ServiceResponse<bool>> ValidateBusinessRuleAsync(int entityId);
} 