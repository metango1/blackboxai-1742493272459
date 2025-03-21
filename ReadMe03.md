# H82Travels - ASP.NET Core 8 Enterprise Application

## Opening in Visual Studio 2022

1. **Open the Solution**
   - Launch Visual Studio 2022
   - Click "Open a project or solution"
   - Navigate to the project folder
   - Select "H82Travels.sln"

2. **Restore NuGet Packages**
   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Wait for the restore process to complete

3. **Database Setup**
   - Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
   - Run the following commands:
     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```

4. **Configure Connection String**
   - Open appsettings.json
   - Update the DefaultConnection string if needed:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=H82Travels;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

5. **Build the Solution**
   - Click Build > Build Solution (or press Ctrl+Shift+B)
   - Ensure there are no build errors

6. **Run the Application**
   - Press F5 to run in debug mode
   - Or press Ctrl+F5 to run without debugging

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

## Required NuGet Packages
All necessary packages are included in the .csproj file:
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.0)
- Microsoft.EntityFrameworkCore.Tools (8.0.0)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (8.0.0)

## Features

### HR Visibility
- Company-wide staff view for CEO, COO, HR Manager
- Country-level view for country HR managers
- Province-level view for provincial HR managers
- Branch-level view for branch managers

### Leave Management
- Branch manager approval workflow
- Country Head Office notification
- CEO/COO override capabilities

### Inventory Issuance
- Request submission system
- Multi-level approval process
- Receipt management
- Fund transfer tracking

## Authorization Strategy

1. **Role-Based Authorization**
   - Predefined roles: CEO, COO, HR Manager, Branch Manager, etc.
   - Super rights for CEO and COO

2. **Claim-Based Authorization**
   - Geographical claims (Country, Province, City)
   - Role-specific claims

3. **Policy-Based Authorization**
   - Complex authorization scenarios
   - Combined role and geographical checks

## Troubleshooting

1. **Database Connection Issues**
   - Verify connection string in appsettings.json
   - Ensure SQL Server is running
   - Check Windows Authentication settings

2. **Build Errors**
   - Clean solution (Build > Clean Solution)
   - Rebuild solution
   - Restore NuGet packages

3. **Runtime Errors**
   - Check application logs
   - Verify database migrations
   - Ensure all services are properly registered

## Support

For technical support or questions, contact:
- Email: support@h82travels.com
- Documentation: [link-to-documentation]
- Issue Tracker: [link-to-issue-tracker]

## License

This project is licensed under the MIT License - see the LICENSE file for details.
