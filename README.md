# Pro.Structure - Project Management System

A comprehensive project management system built with .NET 9.0, implementing modern development practices and clean architecture principles.

## Features

- Project lifecycle management with automated project number generation
- Customer relationship management
- Project manager assignment and tracking
- Service rate and billing management
- Status tracking for projects
- Modern web interface with responsive design
- RESTful API for system integration

## Project Structure

The solution follows Clean Architecture principles and is organized into the following projects:

### Pro.Structure.Core
Core domain layer containing business entities, interfaces, and models. See [Core README](Pro.Structure.Core/README.md) for details.

### Pro.Structure.Infrastructure
Implementation of data access and business logic. See [Infrastructure README](Pro.Structure.Infrastructure/README.md) for details.

### Pro.Structure.Web
Main web interface with MVC pattern. See [Web README](Pro.Structure.Web/README.md) for details.

### Pro.Structure.Web.Api
RESTful API interface. See [Web.Api README](Pro.Structure.Web.Api/README.md) for details.

## Technical Stack

- .NET 9.0
- Entity Framework Core with SQLite
- ASP.NET Core MVC & Web API
- Bootstrap 5.3.3
- Clean Architecture
- SOLID Principles
- Factory Pattern

## Getting Started

1. Clone the repository
2. Ensure .NET 9.0 SDK is installed
3. Run the following commands in the solution directory:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project Pro.Structure.Web
   ```
4. Access the web interface at `https://localhost:7211`
5. Access the API at `https://localhost:5281`

## Development Tools

- Visual Studio 2022 or JetBrains Rider
- SQLite browser (optional)
- Postman or similar for API testing

## AI Assistance Acknowledgment

This project was developed with assistance from AI tools:

- **Claude AI (Anthropic)**: Helped with:
  - Project structure and architecture decisions
  - Implementation of Clean Architecture patterns
  - Creation of views and controllers
  - Documentation generation
  - Bug fixing and code optimization

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details. 