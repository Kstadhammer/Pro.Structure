using Pro.Structure.Infrastructure;

// Create the web application builder
var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
await builder.Services.AddInfrastructureAsync(builder.Configuration);

// Add MVC with runtime compilation for development
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Use error handling and HSTS in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enable HTTP Strict Transport Security
}

// Configure middleware pipeline
app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles(); // Serve static files (CSS, JS, images)
app.UseRouting(); // Enable routing
app.UseAuthorization(); // Enable authorization

// Configure default route to Projects/Index
app.MapControllerRoute(name: "default", pattern: "{controller=Projects}/{action=Index}/{id?}");

// Start the application
app.Run();
