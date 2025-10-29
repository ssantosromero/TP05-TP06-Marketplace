using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Marketplace.Api.Services;
using Marketplace.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Marketplace API",
        Version = "v1"
    });
});

// 🔹 Conexión a la base de datos SQLite
builder.Services.AddDbContext<MarketplaceDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// 🔹 Permitir CORS para React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Servicios propios
builder.Services.AddSingleton<ProductCatalog>();
builder.Services.AddSingleton<CartService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketplace API v1");
    });
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.MapControllers();

// 🔹 Ejecutar el seed al iniciar la app
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();
    context.Database.EnsureCreated();  // crea la BD si no existe
    DbInitializer.Seed(context);       // siembra los productos iniciales
}

app.Run();
