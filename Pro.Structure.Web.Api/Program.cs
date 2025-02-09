using Pro.Structure.Infrastructure;

// Create the API application builder
var builder = WebApplication.CreateBuilder(args);

// Register infrastructure services
await builder.Services.AddInfrastructureAsync(builder.Configuration);

// Add API controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Pro.Structure API", Version = "v1" });
    c.EnableAnnotations();
});

// Configure CORS for cross-origin requests
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure middleware pipeline
app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseCors("AllowAll"); // Enable CORS
app.UseAuthorization(); // Enable authorization

// Map API controllers
app.MapControllers();

// Start the application
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
