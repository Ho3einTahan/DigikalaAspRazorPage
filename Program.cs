using digikala_netCore.Data;
using digikala_netCore.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Authentication and Cookie Settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/AccessDenied";
    options.LogoutPath = "/Account/logout";
    options.Cookie.Name = "AuthCookie";
    options.LoginPath = "/Account/login";
});


builder.Services.Configure<IdentityOptions>(options =>
 {
     // Password Setting
     options.Password.RequireDigit = false;
     options.Password.RequireLowercase = true;
     options.Password.RequireNonAlphanumeric = false;
     options.Password.RequireUppercase = false;
     options.Password.RequiredLength = 8;
     options.Password.RequiredUniqueChars = 2;
 });


// Setting up the DbContext with the default connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Setting up Identity with User and Role management
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Razor Pages to the container
builder.Services.AddRazorPages();

// Register Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var scope = app.Services.CreateAsyncScope();
await Seed.SeedData(scope.ServiceProvider);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Register To Authenticate User
app.UseAuthentication();

// Register Authorization Middleware
app.UseAuthorization();

// Register Razor Pages for all routes
app.MapRazorPages();

app.Run();