using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using H82Travels.Data;
using H82Travels.Models;
using H82Travels.Services;
using H82Travels.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHRService, HRService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Super Rights Policy for CEO and COO
    options.AddPolicy("SuperRights", policy =>
        policy.RequireRole("CEO", "COO"));

    // Leave Approval Policy
    options.AddPolicy("LeaveApproval", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("CEO") ||
            context.User.IsInRole("COO") ||
            context.User.IsInRole("CountryHead")));

    // Country Level Access Policy
    options.AddPolicy("CountryAccess", policy =>
        policy.RequireClaim("Country"));

    // Province Level Access Policy
    options.AddPolicy("ProvinceAccess", policy =>
        policy.RequireClaim("Province"));

    // City Level Access Policy
    options.AddPolicy("CityAccess", policy =>
        policy.RequireClaim("City"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Custom Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed initial data (if needed)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        context.Database.Migrate();
        // Add data seeding here if needed
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();