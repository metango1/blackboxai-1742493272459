# H82Travels - ASP.NET Core 8 Enterprise Application

## Project Overview
H82Travels is an enterprise-level application designed for an airline ticketing firm with branches across UAE, KSA, and Pakistan. The application manages HR operations, leave management, and inventory issuance across different geographical hierarchies.

### Key Features
- **HR Visibility**: Hierarchical staff visibility based on geographical location and role
- **Leave Management**: Streamlined leave request and approval workflow
- **Inventory Issuance**: Structured inventory request and procurement process
- **Role-Based Access**: Comprehensive authorization system with role and location-based permissions

## Prerequisites
- Visual Studio 2022 (Professional or Enterprise)
- .NET 8.0 SDK
- SQL Server 2019 or later
- Git (for version control)

## Required NuGet Packages
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="8.0.0" />
```

## Installation Steps

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd H82Travels
   ```

2. **Database Setup**
   - Open Package Manager Console in Visual Studio
   - Run the following commands:
     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```

3. **Configure Application Settings**
   - Update connection string in appsettings.json
   - Configure any environment-specific settings

4. **Run the Application**
   - Press F5 in Visual Studio 2022
   - Or use `dotnet run` from the command line

## Project Structure

### Core Components

1. **Controllers/**
   - AccountController.cs - Authentication & user management
   - HomeController.cs - Dashboard and main navigation
   - HRController.cs - HR visibility and management
   - LeaveController.cs - Leave request workflow
   - InventoryController.cs - Inventory management

2. **Models/**
   - ApplicationUser.cs - Extended Identity user
   - Branch.cs - Branch information
   - LeaveRequest.cs - Leave management
   - InventoryRequest.cs - Inventory tracking

3. **Services/**
   - AuthService.cs - Authentication logic
   - HRService.cs - HR operations
   - LeaveService.cs - Leave management
   - InventoryService.cs - Inventory operations

4. **Views/**
   - Shared layouts
   - Module-specific views
   - Partial views for reusable components

### Authorization Strategy

1. **Role-Based Authorization**
   - Predefined roles: CEO, COO, HR Manager, Branch Manager, etc.
   - Super rights for CEO and COO

2. **Claim-Based Authorization**
   - Geographical claims (Country, Province, City)
   - Role-specific claims

3. **Policy-Based Authorization**
   - Complex authorization scenarios
   - Combined role and geographical checks

## Development Process

### Phase 1: Initial Setup
- Project creation and structure setup
- Database context and Identity configuration
- Basic authentication implementation

### Phase 2: Core Features
- HR visibility implementation
- Leave management system
- Inventory request workflow

### Phase 3: Authorization
- Role configuration
- Policy implementation
- Claim management

### Phase 4: UI/UX
- Responsive layout implementation
- Module-specific views
- Interactive components

## Error Handling

The application implements comprehensive error handling:
- Global exception handling middleware
- Logging system integration
- User-friendly error pages
- Validation feedback

## Security Considerations

- HTTPS enforcement
- Anti-forgery token implementation
- Secure password policies
- Role-based access control
- Data validation and sanitization

## Testing

To run the tests:
1. Open Test Explorer in Visual Studio
2. Build the solution
3. Run all tests

## Deployment

1. **Preparation**
   - Update connection strings
   - Configure environment variables
   - Set up SSL certificates

2. **Publishing**
   - Right-click on the project in Visual Studio
   - Select "Publish"
   - Follow the deployment wizard

## Contributing

1. Create a feature branch
2. Commit your changes
3. Push to the branch
4. Create a Pull Request

## Support

For support, please contact [support@h82travels.com]

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Views and Controllers
- **Views**: The application includes views for Home, Account (Login and Register), HR, Leave, and Inventory.
- **Controllers**: Each controller handles requests for its respective module, managing data flow and user interactions.