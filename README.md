# Mattin-Lassei Group AB - Project Management System

A comprehensive project management system built with .NET 9.0, implementing modern development practices, clean architecture principles, and a responsive dark mode UI.

## Assignment Requirements

### Required Features (G)
- [x] Frontend Application
  - [x] Project listing page
  - [x] Project creation page
  - [x] Project edit/update page

- [x] Class Library Implementation
  - [x] Entity Framework Core - Code First
  - [x] SQLite database implementation
  - [x] Multiple entities/tables (Projects, Customers, etc.)
  - [x] Service layer implementation
  - [x] Repository pattern implementation
  - [x] Dependency Injection
  - [x] Single Responsibility Principle (SRP)

### Advanced Features (VG)
- [x] Generic Base Repository
- [x] Extended Entity Model
  - [x] Customer entity
  - [x] Project Manager entity
  - [x] Status entity
  - [x] Project entity with relationships
- [x] Comprehensive Service Layer
  - [x] CRUD operations for all entities
  - [x] Business logic implementation
- [x] SOLID Principles Implementation
- [x] Factory Pattern Implementation
- [x] Asynchronous Operations
- [x] Transaction Management

## Implementation Details

### Personal Contributions (50%)
- Backend Architecture & Design
  - Designed and implemented the core domain entities and interfaces
  - Created the repository pattern implementation
  - Developed the service layer with business logic
  - Set up Entity Framework Core with SQLite integration
  - Implemented dependency injection and service registration
- Database Design & Management
  - Designed the database schema and relationships
  - Created migrations and seeding functionality
  - Implemented data access patterns and optimizations
- Business Logic Implementation
  - Developed core business rules and validations
  - Implemented project status workflow logic
  - Created project manager availability tracking
  - Built customer relationship management features

### AI-Assisted Components (50%)
- Transaction Management Implementation
  - Implementation of the Unit of Work pattern
  - Transaction handling and rollback mechanisms
  - Integration with Entity Framework Core
  - Atomic operation handling
- Frontend Development
  - Implementation of responsive Bootstrap-based UI
  - Dark mode theming and customization
  - Status badge styling and visual components
  - Form validation and user input handling
- Documentation & Code Organization
  - README.md documentation and structure
  - Code comments and documentation
  - Project structure organization
  - API documentation with Swagger
- UI/UX Enhancements
  - Responsive design implementation
  - Dark/light mode toggle functionality
  - Interactive status badges and indicators
  - Form validation feedback
- Code Quality & Optimization
  - Code refactoring suggestions
  - Performance optimization recommendations
  - Best practices implementation
  - Error handling improvements
- Testing & Debugging
  - Bug identification and fixes
  - Testing strategy suggestions
  - Error handling patterns
  - Edge case handling

## Features

### Core Functionality
- Project lifecycle management with automated project number generation
- Customer relationship management
- Project manager workload tracking and assignment
- Dynamic status tracking for projects
- Service rate and billing management

### Technical Features
- Clean Architecture with domain-driven design
- Modern web interface with responsive design
- Dark/Light mode theme support
- RESTful API with Swagger documentation
- Factory pattern implementation
- Service response pattern for robust error handling

## Project Structure

The solution follows Clean Architecture principles and is organized into the following projects:

### Pro.Structure.Core
Core domain layer containing business entities, interfaces, and models. See [Core README](Pro.Structure.Core/README.md) for details.
- Business entities and validation
- Interface definitions
- Domain models and DTOs
- Factory interfaces

### Pro.Structure.Infrastructure
Implementation of data access and business logic. See [Infrastructure README](Pro.Structure.Infrastructure/README.md) for details.
- Entity Framework Core implementation
- Repository implementations
- Service implementations
- Factory implementations
- Database migrations and seeding

### Pro.Structure.Web
Main web interface with MVC pattern. See [Web README](Pro.Structure.Web/README.md) for details.
- Responsive Bootstrap 5.3.3 UI
- Dark/Light mode support
- CRUD operations
- Form validation
- Status tracking

### Pro.Structure.Web.Api
RESTful API interface. See [Web.Api README](Pro.Structure.Web.Api/README.md) for details.
- RESTful endpoints
- Swagger/OpenAPI documentation
- CORS configuration
- API versioning

## Technical Stack

### Backend
- .NET 9.0
- Entity Framework Core with SQLite
- ASP.NET Core MVC & Web API
- Clean Architecture
- SOLID Principles
- Factory Pattern
- Service Response Pattern

### Frontend
- Bootstrap 5.3.3
- Bootstrap Icons
- Modern responsive design
- Dark/Light mode theming
- jQuery validation

## Getting Started

1. Clone the repository
   ```bash
   git clone https://github.com/Kstadhammer/Pro.Structure.git
   ```

2. Ensure .NET 9.0 SDK is installed

3. Run the following commands in the solution directory:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project Pro.Structure.Web
   ```

4. Access the applications:
   - Web Interface: `https://localhost:7211`
   - API: `https://localhost:5281`
   - Swagger Documentation: `https://localhost:5281/swagger`

## Development Setup

### Required Tools
- Visual Studio 2022 or JetBrains Rider
- .NET 9.0 SDK
- SQLite browser (optional)
- Git

### Database Setup
The application uses SQLite with automatic migrations:
1. Database will be created automatically on first run
2. Initial seed data will be populated
3. Migrations are applied automatically

## Features in Detail

### Project Management
- Automated project number generation
- Status tracking with customizable statuses
- Financial tracking (hourly rates, total price)
- Project manager assignment with workload balancing

### Customer Management
- Customer profile management
- Project association tracking
- Contact information management

### Project Manager Features
- Workload tracking
- Project assignment limits
- Contact information management

### Status Management
- Dynamic status creation
- Project status tracking
- Status-based filtering

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Built with assistance from Claude AI (Anthropic) for implementing transaction management and other features
- Guidance and mentoring from Illir on transaction management implementation and best practices
- Bootstrap and Bootstrap Icons for UI components
- Entity Framework Core for data access
- ASP.NET Core team for the framework

## Transaction Management

The project implements robust transaction management using the Unit of Work pattern, developed with assistance from AI and guidance from Illir. Key features include:

### Core Components
- `IUnitOfWork` interface defining transaction operations
- `UnitOfWork` implementation handling transaction lifecycle
- Integration with Entity Framework Core's transaction system
- Automatic rollback on failure

### Key Benefits
- Ensures data consistency across multiple operations
- Provides automatic rollback on failures
- Maintains ACID properties for database operations
- Simplifies complex transaction management

### Usage Example
```csharp
// Example of transaction usage in services
public async Task<ServiceResponse<T>> Operation()
{
    return await _unitOfWork.ExecuteInTransactionAsync(async () =>
    {
        // Multiple database operations
        // All succeed or all fail together
    });
}
``` 