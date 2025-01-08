# README: Digikala Online Store Project

## Table of Contents
1. **Introduction**
2. **Prerequisites**
3. **Setting Up the Project**
4. **Database Configuration**
5. **Creating Models and DbContext**
6. **Identity Configuration**
7. **Creating Razor Pages**
8. **Running Migrations**
9. **Testing the Project**
10. **Troubleshooting**
11. **Development Team**

---

## 1. Introduction
The Digikala Online Store project is built using **ASP.NET Core** and **Razor Pages**. It includes features such as product management, user authentication, shopping cart functionality, and online payments.

---

## 2. Prerequisites
To run and develop this project, ensure you have the following:

- **.NET 6 or later** installed on your system.
- **Visual Studio** or **VS Code** for development.
- **SQL Server** or any other preferred database.
- **Entity Framework Core Tools** for database migrations.

---

## 3. Setting Up the Project

1. Create a new project using the terminal:
   ```bash
   dotnet new razor -n digikala-netCore
   ```

2. Navigate to the project directory:
   ```bash
   cd digikala-netCore
   ```

3. Install the necessary NuGet packages:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

---

## 4. Database Configuration

1. Add the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   }
   ```

2. Update `Program.cs` to configure the database context:
   ```csharp
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   builder.Services.AddDefaultIdentity<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
   ```

---

## 5. Creating Models and DbContext

1. Create a new folder named `Models` and add a `Product` model:
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

2. Define `ApplicationDbContext` in the `Data` folder:
   ```csharp
   public class ApplicationDbContext : IdentityDbContext<IdentityUser>
   {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

       public DbSet<Product> Products { get; set; }
   }
   ```

---

## 6. Identity Configuration

To configure **Identity** and handle user authentication, follow these steps:

1. Add Identity setup in `Program.cs`:
   ```csharp
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   builder.Services.AddDefaultIdentity<IdentityUser>(options =>
       options.SignIn.RequireConfirmedAccount = true)
       .AddEntityFrameworkStores<ApplicationDbContext>();
   ```

2. Use `UserManager` and `RoleManager` in your services to manage user roles and permissions effectively:
   ```csharp
   public class RoleInitializer
   {
       public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
       {
           if (!await roleManager.RoleExistsAsync("Admin"))
           {
               await roleManager.CreateAsync(new IdentityRole("Admin"));
           }

           if (!await roleManager.RoleExistsAsync("User"))
           {
               await roleManager.CreateAsync(new IdentityRole("User"));
           }
       }
   }
   ```

---

## 7. Creating Razor Pages

Create Razor Pages to manage products:

1. Add a new Razor Page for products in the `Pages` folder.
2. Define the logic in `Product.cshtml.cs`:
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
3. Design the layout in `Product.cshtml` to display the products.

---

## 8. Running Migrations

1. Create a new migration:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
2. Update the database:
   ```bash
   dotnet ef database update
   ```

---

## 9. Testing the Project

1. Run the project:
   ```bash
   dotnet run
   ```
2. Navigate to the relevant Razor Pages to test the functionality.

---

## 10. Troubleshooting

1. **Error: Namespace not found**
   - Ensure all necessary `using` directives are included.

2. **Build errors**
   - Run `dotnet build` to identify and fix issues.

3. **Database connection issues**
   - Verify the connection string in `appsettings.json`.

---

## 11. Development Team

This project was developed by:

- **Hosein Tahan**
- **Amir Mohsen Rahi**
- **Matin Abbas Doost**

**Developed by:** Ho3einTahan

