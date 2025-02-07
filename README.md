# Pro.Structure - Project Management System

A project management system built with .NET 9.0, implementing modern development practices and clean architecture principles.

## Features

- Project lifecycle management with automated project number generation
- Customer relationship management
- Project manager assignment and tracking
- Service rate and billing management
- Status tracking for projects

## Technical Stack

- .NET 9.0
- Entity Framework Core
- SQLite Database
- Clean Architecture
- SOLID Principles
- Factory Pattern
- Repository Pattern
- Generic Base Classes
- Async/Await Implementation

## Project Structure

The solution follows a clean architecture pattern with three main projects:

- **Pro.Structure.Core**: Contains domain models, interfaces, and business logic definitions
  - Entities
  - Interfaces
  - Models (DTOs)
  - Factories

- **Pro.Structure.Infrastructure**: Implements data access and business logic
  - Entity Framework Core configuration
  - Repositories
  - Services
  - Database context
  - Migrations

- **Pro.Structure.Web** (Coming soon): Will handle the user interface
  - Controllers
  - Views
  - ViewModels
  - Frontend assets

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or JetBrains Rider

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Kstadhammer/Pro.Structure.git
   ```

2. Navigate to the project directory:
   ```bash
   cd Pro.Structure
   ```

3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

4. Update the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

## Architecture

- **Clean Architecture**: The solution follows clean architecture principles with clear separation of concerns
- **SOLID Principles**: Implementation adheres to SOLID principles
- **Repository Pattern**: Generic repository pattern for data access
- **Factory Pattern**: Used for mapping between entities and DTOs
- **Service Layer**: Business logic implementation
- **Entity Framework Core**: Code-first approach with SQLite

## Database Schema

- **Projects**: Main project information
- **Customers**: Customer details
- **ProjectManagers**: Project manager information
- **Statuses**: Project status definitions

## Contributing

This is a school project for EC Education. Contributions are welcome for educational purposes.

## License

This project is licensed under the MIT License. 