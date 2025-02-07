# Pro.Structure.Web.Api

RESTful API implementation for the Pro.Structure application, providing programmatic access to all system functionality. Built with ASP.NET Core Web API, this project enables integration with external systems and custom clients.

## Project Structure

### Controllers
- `ProjectsController.cs`: Project management endpoints
- `CustomersController.cs`: Customer operations
- `ProjectManagersController.cs`: Project manager endpoints
- `StatusController.cs`: Status management

### DTOs
- Request/response models
- Validation attributes
- API-specific data contracts
- Mapping configurations

### Middleware
- Exception handling
- Request/response logging
- API versioning
- Performance monitoring

## API Endpoints

### Projects
```
GET    /api/projects
GET    /api/projects/{id}
POST   /api/projects
PUT    /api/projects/{id}
DELETE /api/projects/{id}
```

### Customers
```
GET    /api/customers
GET    /api/customers/{id}
POST   /api/customers
PUT    /api/customers/{id}
DELETE /api/customers/{id}
```

### Project Managers
```
GET    /api/project-managers
GET    /api/project-managers/{id}
POST   /api/project-managers
PUT    /api/project-managers/{id}
DELETE /api/project-managers/{id}
```

### Statuses
```
GET    /api/statuses
GET    /api/statuses/{id}
POST   /api/statuses
PUT    /api/statuses/{id}
DELETE /api/statuses/{id}
```

## Features

- RESTful API design
- Swagger/OpenAPI documentation
- JWT authentication ready
- Rate limiting
- CORS configuration
- API versioning
- Response caching

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
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": ["https://localhost:7211"]
  }
}
```

## Security

- HTTPS enforcement
- API key authentication ready
- Cross-Origin Resource Sharing (CORS)
- Input validation
- Rate limiting
- Request size limits

## Response Format

### Success Response
```json
{
  "success": true,
  "data": {},
  "message": null
}
```

### Error Response
```json
{
  "success": false,
  "data": null,
  "message": "Error description"
}
```

## Development

### Requirements
- Visual Studio 2022 or Rider
- .NET 9.0 SDK
- Postman or similar for testing

### Running Locally
```bash
dotnet restore
dotnet build
dotnet run
```

Access the API at `https://localhost:5281`
Swagger UI available at `https://localhost:5281/swagger`

## Testing

- Integration tests for all endpoints
- Postman collection available
- Example requests and responses
- Authentication testing
- Performance testing

## Documentation

- Swagger UI for interactive documentation
- OpenAPI specification
- Detailed endpoint descriptions
- Request/response examples
- Authentication guide 