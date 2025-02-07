# Mattin-Lassei Group AB - Project Management System

A comprehensive project management system built with .NET 9.0, implementing modern development practices, clean architecture principles, and a responsive dark mode UI.

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

- Built with assistance from Claude AI (Anthropic)
- Bootstrap and Bootstrap Icons for UI components
- Entity Framework Core for data access
- ASP.NET Core team for the framework 