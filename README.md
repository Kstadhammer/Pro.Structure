# Project Management System

A modern project management system built for Mattin-Lassei Group AB. The system helps manage projects, customers, and project managers with an easy-to-use interface and robust security features.

## Features

### Project Management
- Create and manage projects with automatic project number generation (P-101, P-102, etc.)
- Track project status (not started, in progress, completed, etc.)
- Monitor project costs and billing rates
- Assign project managers with workload balancing (max 5 active projects)
- Track project timelines and deadlines

### Customer Management
- Store and manage customer information
- Track customer projects and history
- Manage customer contacts
- View project timelines and costs
- Real-time project status updates

### Project Manager Features
- Track workload and capacity
- Manage multiple projects (up to 5 active)
- Update project status and progress
- View deadlines and milestones
- Handle project assignments

### Security Features
- User authentication with secure password hashing
- Role-based authorization
- Account lockout after failed attempts
- Remember me functionality
- Secure password reset
- Profile management

### User Interface
- Modern, responsive Bootstrap 5.3.3 design
- Dark/light mode with smooth transitions
- Interactive loading states
- Form validation with feedback
- Breadcrumb navigation
- Mobile-friendly layout

## Technical Stack

### Frontend
- ASP.NET Core MVC
- Bootstrap 5.3.3
- Bootstrap Icons
- jQuery validation
- Responsive design
- Dark/light theme support

### Backend
- .NET 9.0
- Entity Framework Core
- SQLite database
- Repository pattern
- Unit of Work pattern
- Service layer architecture

### Security
- Cookie authentication
- Password hashing with BCrypt
- CSRF protection
- Secure cookie configuration
- Input validation
- XSS protection

## Project Structure

### Core (Pro.Structure.Core)
- Entities and models
- Interfaces
- Business rules
- Validation logic

### Infrastructure (Pro.Structure.Infrastructure)
- Database context and migrations
- Repositories implementation
- Services implementation
- Data seeding
- Transaction management

### Web (Pro.Structure.Web)
- MVC controllers
- Razor views
- ViewModels
- Client-side assets
- Authentication/Authorization

### API (Pro.Structure.Web.Api)
- REST endpoints
- Swagger documentation
- CORS configuration
- Rate limiting

## Development Setup

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or JetBrains Rider
- SQLite browser (optional)
- Git

### Getting Started
1. Clone the repository
2. Open the solution in your IDE
3. Run database migrations:
   ```bash
   dotnet ef database update -p Pro.Structure.Infrastructure -s Pro.Structure.Web
   ```
4. Run the application:
   ```bash
   dotnet run -p Pro.Structure.Web
   ```

### Default Admin Account
- Email: admin@prostructure.com
- Password: Admin123!

## Development Credits

### AI Assistance (Claude)
The following features were developed with AI assistance:

1. **Authentication & Authorization**
   - User registration and login
   - Password hashing and security
   - Role-based access control
   - Remember me functionality
   - Account lockout system

2. **User Interface**
   - Dark/light mode implementation
   - Responsive design
   - Form validation
   - Loading states
   - Navigation system

3. **Database Design**
   - Entity relationships
   - Migration setup
   - Data seeding
   - Transaction handling

4. **Architecture**
   - Repository pattern
   - Unit of Work pattern
   - Service layer
   - Dependency injection

5. **Security Features**
   - Password hashing
   - CSRF protection
   - Cookie security
   - Input validation
   - XSS protection

### Personal Implementation
The following features were implemented without AI assistance:

1. **Business Logic**
   - Project workflow rules
   - Customer management
   - Project manager workload
   - Status management

2. **Data Management**
   - Data structure design
   - Query optimization
   - Performance tuning
   - Data validation rules

3. **Testing**
   - Unit tests
   - Integration tests
   - User acceptance testing
   - Performance testing

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details. 