# Project Management System

A simple and modern project management system built for Mattin-Lassei Group AB. The system helps manage projects, customers, and project managers with an easy-to-use interface.

## What Can This System Do?

### For Projects
- Create and manage projects
- Automatically generate project numbers (format: P-101, P-102, etc.)
- Track project status (not started, in progress, completed, etc.)
- Monitor project costs and billing rates (per hour/project)
- Assign project managers (with workload balancing)
- Track project timelines and deadlines
- Generate project reports
- Manage project budgets and cost tracking

### For Customers
- Store customer information (name, email, phone)
- Track customer projects (current and historical)
- Manage customer contacts and communication
- View project history and timeline
- Monitor project costs and budgets
- Access real-time project status updates
- Store billing information and rates

### For Project Managers
- Track workload (max 5 active projects)
- Manage multiple projects efficiently
- Keep contact information up to date
- Monitor team capacity and availability
- View project deadlines and milestones
- Handle project assignments and transfers
- Track project progress and status updates

## System Architecture

### Main Components
1. **Website (MVC Application)**
   - Built with ASP.NET Core MVC
   - Responsive Bootstrap 5.3.3 UI
   - Dark/light mode using CSS variables
   - Client-side validation with jQuery
   - Real-time updates using AJAX

2. **API (RESTful Service)**
   - ASP.NET Core Web API
   - Swagger/OpenAPI documentation
   - CORS enabled for cross-origin requests
   - JWT authentication ready
   - Rate limiting and caching support

3. **Database (SQLite)**
   - Entity Framework Core with Code First
   - Automatic migrations
   - Data seeding for initial setup
   - Transaction management
   - Relationship handling

4. **Business Logic Layer**
   - Service-based architecture
   - Repository pattern implementation
   - Factory pattern for object creation
   - Unit of Work for transactions
   - SOLID principles application

### Technical Implementation

#### Database Schema
```
Projects
- Id (int, primary key)
- ProjectNumber (string, unique)
- Name (string, max 100)
- StartDate (DateTime)
- EndDate (DateTime)
- Rate (decimal)
- TotalPrice (decimal)
- CustomerId (int, foreign key)
- ProjectManagerId (int, foreign key)
- StatusId (int, foreign key)

Customers
- Id (int, primary key)
- Name (string, max 100)
- Email (string, unique)
- PhoneNumber (string)

ProjectManagers
- Id (int, primary key)
- FirstName (string)
- LastName (string)
- Email (string, unique)
- PhoneNumber (string)

Statuses
- Id (int, primary key)
- Name (string, max 50)
- Description (string)
```

#### Key Features Implementation

1. **Transaction Management**
   ```csharp
   public async Task<ServiceResponse<T>> Operation()
   {
       return await _unitOfWork.ExecuteInTransactionAsync(async () =>
       {
           // Multiple database operations
           // All succeed or all fail together
       });
   }
   ```

2. **Project Number Generation**
   ```csharp
   public async Task<string> GenerateProjectNumberAsync()
   {
       var lastProject = await _dbSet
           .OrderByDescending(p => p.ProjectNumber)
           .FirstOrDefaultAsync();

       if (lastProject == null)
           return "P-101";

       var currentNumber = int.Parse(lastProject.ProjectNumber.Split('-')[1]);
       return $"P-{currentNumber + 1}";
   }
   ```

3. **Workload Management**
   ```csharp
   public async Task<bool> CanAssignProjectAsync(int projectManagerId)
   {
       var activeProjects = await GetActiveProjectsCount(projectManagerId);
       return activeProjects < 5;
   }
   ```

## Development Contributions

### AI-Assisted Features (50%)
1. **Transaction Management**
   - Implementation of Unit of Work pattern
   - Transaction handling and rollback
   - Data consistency protection
   - Error recovery mechanisms

2. **User Interface**
   - Responsive design implementation
   - Dark/light mode theming
   - Form validations
   - Status indicators
   - Interactive components

3. **Code Architecture**
   - Repository pattern setup
   - Service layer implementation
   - Factory pattern integration
   - Dependency injection configuration

4. **Documentation**
   - API documentation with Swagger
   - Code comments and explanations
   - README file structure
   - Setup instructions

5. **Database Design**
   - Entity relationships
   - Data migrations
   - Seeding functionality
   - Query optimization

### Personal Implementation (50%)
1. **Core Business Logic**
   - Project management rules
   - Customer relationship handling
   - Project manager workload balancing
   - Status workflow management

2. **Database Management**
   - Data structure design
   - Table relationships
   - Data access patterns
   - Performance optimization

3. **Security Features**
   - Input validation
   - Data protection
   - Error handling
   - Safe data operations

## Getting Started

### Prerequisites
- Visual Studio 2022 or JetBrains Rider
- .NET 9.0 SDK
- Git for version control
- SQLite browser (optional but helpful)
- Node.js (for frontend development)
- Web browser (Chrome/Firefox/Edge latest version)

### Development Environment Setup
1. Install .NET 9.0 SDK
2. Install your preferred IDE
3. Install SQLite browser
4. Clone the repository
5. Set up user secrets (if needed)
6. Run database migrations

### Configuration Settings
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ProStructure.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Project Organization

### Core (Pro.Structure.Core)
- Basic building blocks
- Business rules
- Interfaces
- Data models
- Validation logic

### Infrastructure (Pro.Structure.Infrastructure)
- Database operations
- Business logic
- Data access
- Transaction management
- Repository implementations

### Website (Pro.Structure.Web)
- User interface
- Forms and pages
- Dark/light mode
- Client-side validation
- Interactive features

### API (Pro.Structure.Web.Api)
- API endpoints
- Documentation
- External system connections
- CORS configuration
- Swagger integration

## Special Features

### Data Safety
- All changes are wrapped in transactions
- Automatic rollback if something goes wrong
- Data consistency protection
- Safe multi-step operations
- Error logging and tracking
- Data validation at multiple levels

### Smart Design
- Clean and organized code
- Easy to maintain and update
- Follows best practices
- Built for performance
- Modular architecture
- Extensible design

### User Experience
- Responsive layout for all devices
- Intuitive navigation
- Dark/light mode support
- Fast loading times
- Interactive feedback
- Form validation
- Error messages
- Success notifications

### Database Features
- Automatic migrations
- Data seeding
- Relationship management
- Transaction support
- Query optimization
- Data consistency rules
- Backup support

## Help and Credits

This project was built with significant help from:

### AI Assistance (Claude)
- Implemented transaction management system
- Designed database structure
- Created repository patterns
- Set up dependency injection
- Generated API documentation
- Implemented form validation
- Created error handling
- Designed user interface components
- Wrote code comments and documentation
- Suggested performance optimizations

### Professional Guidance (Illir)
- Transaction management best practices
- Database design review
- Architecture recommendations
- Code quality improvements
- Performance optimization tips
- Security considerations

### Technologies Used
- Bootstrap - For responsive UI
- Entity Framework Core - Database operations
- ASP.NET Core - Web framework
- SQLite - Database engine
- Swagger - API documentation
- Git - Version control

## Want to Help?

1. Fork the project
2. Create your feature branch
3. Make your changes
4. Test everything works
5. Create a pull request

### Areas for Contribution
- New features
- Bug fixes
- Documentation improvements
- Performance optimizations
- Test coverage
- UI/UX enhancements

## License

This project uses the MIT License - see [LICENSE](LICENSE) for details. 