# README: Configuration Guide for ASP.NET Core Project

## Table of Contents
1. Overview
2. Prerequisites
3. Setting Up the Project
4. Configuring the Database
5. Adding Models and DbContext
6. Setting Up Identity
7. Building Razor Pages
8. Running Migrations
9. Testing the Application
10. Troubleshooting

---

## 1. Overview
This document provides a step-by-step guide to configure and build your ASP.NET Core project. It covers the creation of models, DbContext configuration, Razor Pages setup, and database integration.

---

## 2. Prerequisites
- .NET 6 or later installed
- Visual Studio or VS Code
- SQL Server or any preferred database system
- Entity Framework Core tools

---

## 3. Setting Up the Project
1. Open a terminal and create a new project:
   ```bash
   dotnet new razor -n digikala-netCore
   ```
2. Navigate to the project directory:
   ```bash
   cd digikala-netCore
   ```
3. Install necessary NuGet packages:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

---

## 4. Configuring the Database
1. Open `appsettings.json` and add your connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   }
   ```
2. Update `Program.cs` to include the database context:
   ```csharp
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   builder.Services.AddDefaultIdentity<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
   ```

---

## 5. Adding Models and DbContext
1. Create a folder named `Models`.
2. Add a `Product` model:
   ```csharp
   public class Product
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public decimal Price { get; set; }
       public string ImageUrl { get; set; }
   }
   ```
3. Define `ApplicationDbContext` in the `Data` folder:
   ```csharp
   public class ApplicationDbContext : IdentityDbContext<IdentityUser>
   {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

       public DbSet<Product> Products { get; set; }
   }
   ```

---

## 6. Setting Up Identity
1. Add Identity to your project by updating `Program.cs`.
2. Configure services to use default Identity with `ApplicationDbContext`.

---

## 7. Building Razor Pages
1. Add a Razor Page for `Product` in the `Pages` folder:
   ```csharp
   public class ProductModel : PageModel
   {
       private readonly ApplicationDbContext _context;

       public ProductModel(ApplicationDbContext context)
       {
           _context = context;
       }

       public List<Product> Products { get; set; }

       public void OnGet()
       {
           Products = _context.Products.ToList();
       }
   }
   ```
2. Design `Product.cshtml` to display the products.

---

## 8. Running Migrations
1. Add a migration:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
2. Update the database:
   ```bash
   dotnet ef database update
   ```

---

## 9. Testing the Application
1. Run the project:
   ```bash
   dotnet run
   ```
2. Navigate to the `Product` page to ensure data is displayed correctly.

---

## 10. Troubleshooting
1. **Error: Namespace not found**
   - Ensure all necessary `using` directives are added.
2. **Build errors**
   - Run `dotnet build` to identify issues.
3. **Database connection issues**
   - Verify your connection string in `appsettings.json`.

---

Enjoy building your project! If you encounter any issues, feel free to ask for help.

