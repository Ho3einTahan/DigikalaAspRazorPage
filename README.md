
# پروژه فروشگاه آنلاین دیجی‌کالا

## فهرست مطالب
1. **مقدمه**
2. **پیش‌نیازها**
3. **نصب و راه‌اندازی پروژه**
4. **پیکربندی دیتابیس**
5. **ساخت مدل‌ها و DbContext**
6. **پیکربندی Identity**
7. **ایجاد صفحات Razor**
8. **اجرای Migrations و به‌روزرسانی دیتابیس**
9. **آزمایش پروژه**
10. **عیب‌یابی و مشکلات رایج**
11. **اطلاعات تیم توسعه‌دهنده**

---

## 1. مقدمه

پروژه فروشگاه آنلاین دیجی‌کالا با استفاده از **ASP.NET Core** و **Razor Pages** ساخته شده است. این پروژه شامل امکاناتی برای مدیریت محصولات، احراز هویت کاربران، سبد خرید، و پرداخت آنلاین می‌باشد.

---

## 2. پیش‌نیازها

برای راه‌اندازی پروژه، به پیش‌نیازهای زیر نیاز دارید:

- **.NET 6 یا بالاتر** نصب شده بر روی سیستم شما.
- **Visual Studio** یا **VS Code** برای توسعه پروژه.
- **SQL Server** یا هر دیتابیس دیگر.
- **Entity Framework Core Tools** برای اجرای Migrations و ارتباط با دیتابیس.

---

## 3. نصب و راه‌اندازی پروژه

1. ابتدا در ترمینال دستور زیر را وارد کنید تا یک پروژه جدید بسازید:
   ```bash
   dotnet new razor -n digikala-netCore
   ```

2. به پوشه پروژه وارد شوید:
   ```bash
   cd digikala-netCore
   ```

3. پکیج‌های مورد نیاز را نصب کنید:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

---

## 4. پیکربندی دیتابیس

1. در فایل `appsettings.json` اطلاعات اتصال به دیتابیس را وارد کنید:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   }
   ```

2. در `Program.cs`، DbContext را به پروژه اضافه کنید:
   ```csharp
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   builder.Services.AddDefaultIdentity<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
   ```

---

## 5. ساخت مدل‌ها و DbContext

1. یک پوشه جدید به نام `Models` بسازید و مدل `Product` را به صورت زیر ایجاد کنید:
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

2. `ApplicationDbContext` را در پوشه `Data` به شکل زیر تعریف کنید:
   ```csharp
   public class ApplicationDbContext : IdentityDbContext<IdentityUser>
   {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

       public DbSet<Product> Products { get; set; }
   }
   ```

---

## 6. پیکربندی Identity

برای استفاده از **Identity**، در `Program.cs` تنظیمات زیر را اضافه کنید:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ApplicationDbContext>();
```

---

## 7. ایجاد صفحات Razor

برای ایجاد صفحه مدیریت محصولات، ابتدا `Product.cshtml` و `Product.cshtml.cs` را در پوشه `Pages` بسازید.

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

در `Product.cshtml` طراحی کنید که محصولات را نمایش دهد.

---

## 8. اجرای Migrations و به‌روزرسانی دیتابیس

1. برای ایجاد یک migration جدید، دستور زیر را وارد کنید:
   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. سپس دیتابیس را به‌روزرسانی کنید:
   ```bash
   dotnet ef database update
   ```

---

## 9. آزمایش پروژه

برای اجرای پروژه، دستور زیر را وارد کنید:

```bash
dotnet run
```

سپس به صفحه `Product` بروید و اطمینان حاصل کنید که داده‌ها به درستی نمایش داده می‌شوند.

---

## 10. عیب‌یابی و مشکلات رایج

1. **خطای "Namespace not found"**
   - اطمینان حاصل کنید که تمامی `using`های لازم وارد شده باشد.
   
2. **خطای ساخت (Build errors)**
   - دستور `dotnet build` را اجرا کنید تا مشکلات ساخت شناسایی شوند.
   
3. **مشکلات اتصال به دیتابیس**
   - اتصال به دیتابیس را در `appsettings.json` بررسی کنید و اطمینان حاصل کنید که تنظیمات درست هستند.

---

## 11. اطلاعات تیم توسعه‌دهنده

این پروژه توسط تیم زیر توسعه داده شده است:

- **حسین طحان مفرد طاهری** (توسعه‌دهنده اصلی)
- **امیر محسن راهی** (توسعه‌دهنده بخش امنیت)
- **متین عباس دوست** (توسعه‌دهنده بخش طراحی رابط کاربری)

**Developed by**: Ho3einTahan
