# Pro.Structure.Web

The main web interface for the Pro.Structure application, built using ASP.NET Core MVC. This project provides a modern, responsive user interface for managing projects, customers, and project managers.

## Project Structure

### Controllers
- `HomeController.cs`: Landing page and dashboard
- `ProjectsController.cs`: Project management operations
- `CustomersController.cs`: Customer management
- `ProjectManagersController.cs`: Project manager operations
- `StatusController.cs`: Status management

### Views
- Shared layouts and partial views
- CRUD views for all entities
- Dashboard and overview pages
- Error handling pages
- Form components and validation

### ViewModels
- Data models specific to view requirements
- Form models with validation
- Display models for lists and details
- Complex view models for dashboard

### wwwroot
- CSS styles and Bootstrap customization
- JavaScript files and libraries
- Static assets and images
- Font files

## Features

- Responsive Bootstrap-based UI
- Client-side validation
- AJAX operations for better UX
- Rich form components
- Dynamic dropdowns
- Toast notifications
- Error handling and logging

## User Interface

### Project Management
- Project creation with auto-generated numbers
- Status tracking and updates
- Customer and manager assignment
- Financial information tracking
- Project history and audit trail

### Customer Management
- Customer registration
- Project association
- Contact information
- Activity history

### Project Manager Features
- Manager profiles
- Project assignments
- Workload overview
- Performance tracking

## Technologies

- ASP.NET Core MVC
- Bootstrap 5.3.3
- jQuery
- DataTables.net
- Select2
- Toastr notifications

## Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Security

- CSRF protection
- Input validation
- XSS prevention
- Authentication ready
- Authorization framework

## Performance

- Bundling and minification
- Async operations
- Caching where appropriate
- Optimized asset loading
- Lazy loading of data

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers

## Development

### Requirements
- Visual Studio 2022 or Rider
- .NET 9.0 SDK
- Node.js for frontend tools

### Running Locally
```bash
dotnet restore
dotnet build
dotnet run
```

Access the application at `https://localhost:7211` 