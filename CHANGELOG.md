# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-03-14

### Added
- Initial release with core functionality
- Project management features
  - Create and manage projects
  - Automatic project number generation
  - Project status tracking
  - Cost and billing management
- Customer management
  - Customer profiles
  - Project history
  - Contact information
- Project manager features
  - Workload management
  - Project assignment
  - Status updates
- User authentication and authorization
  - User registration and login
  - Role-based access control
  - Remember me functionality
  - Account lockout
  - Password reset
- User interface
  - Responsive Bootstrap 5.3.3 design
  - Dark/light mode
  - Form validation
  - Loading states
  - Breadcrumb navigation
- Database
  - SQLite with Entity Framework Core
  - Code-first migrations
  - Data seeding
  - Transaction management

### Security
- Password hashing with BCrypt
- CSRF protection
- Secure cookie configuration
- Input validation
- XSS protection

## [0.1.0] - 2024-03-07

### Added
- Project setup and architecture
- Basic database structure
- Initial UI components
- Core authentication system 