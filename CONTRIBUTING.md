# Contributing to Pro.Structure

Thank you for your interest in contributing to Pro.Structure! This document provides guidelines and instructions for contributing to the project.

## Code of Conduct

By participating in this project, you agree to maintain a professional and respectful environment. We expect all contributors to:

- Be respectful of differing viewpoints and experiences
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

## How to Contribute

1. Fork the repository
2. Create a new branch for your feature/fix
3. Make your changes
4. Write/update tests as needed
5. Ensure all tests pass
6. Submit a pull request

### Branch Naming

- Feature branches: `feature/your-feature-name`
- Bug fixes: `fix/issue-description`
- Documentation: `docs/what-you-documented`

### Commit Messages

- Use clear, descriptive commit messages
- Start with a verb in present tense (e.g., "Add", "Fix", "Update")
- Reference issue numbers when applicable

Example:
```
Add project status validation (#123)
```

## Development Setup

1. Install prerequisites:
   - .NET 9.0 SDK
   - Visual Studio 2022 or JetBrains Rider
   - SQLite browser (optional)
   - Git

2. Clone your fork:
   ```bash
   git clone https://github.com/YOUR-USERNAME/Pro.Structure.git
   ```

3. Add the upstream remote:
   ```bash
   git remote add upstream https://github.com/ORIGINAL-OWNER/Pro.Structure.git
   ```

4. Create a branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Pull Request Process

1. Update the README.md with details of changes if needed
2. Update the documentation if you're changing functionality
3. Ensure your code follows the existing style
4. Include relevant tests
5. Link any related issues
6. Get a code review from maintainers

## Style Guidelines

### C# Code Style
- Use C# latest language features
- Follow Microsoft's C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and concise

### Testing
- Write unit tests for new features
- Maintain existing tests
- Aim for high test coverage
- Use meaningful test names

### Documentation
- Keep documentation up to date
- Use clear, concise language
- Include code examples when helpful
- Document breaking changes

## Areas for Contribution

### Features
- New project management features
- Reporting capabilities
- User interface improvements
- Performance optimizations

### Documentation
- Code documentation
- User guides
- API documentation
- Example usage

### Testing
- Unit tests
- Integration tests
- Performance tests
- UI/UX testing

## Getting Help

If you need help with your contribution:

1. Check the documentation
2. Look for similar issues
3. Ask questions in pull requests
4. Contact the maintainers

## Recognition

All contributors will be recognized in the project's README.md file.

## License

By contributing to Pro.Structure, you agree that your contributions will be licensed under the MIT License. 