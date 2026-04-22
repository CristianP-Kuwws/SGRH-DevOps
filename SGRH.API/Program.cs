using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys")))
    .SetApplicationName("SGRH.API");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "SGRH API",
            Version = "v1",
            Description = "API para gestion de reservas de hoteles",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "SGRH"
            }
        });
    });
}

ServiceRegistration.AddPersistenceLayerIOC(builder.Services, builder.Configuration);
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IFloorService, FloorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SGRH API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SGRHContext>();
    await context.Database.EnsureCreatedAsync();

    if (!await context.Floors.AnyAsync())
    {
        await context.Floors.AddRangeAsync(
            new Floor { FloorNumber = 1, Description = "Primer piso", Status = "active" },
            new Floor { FloorNumber = 2, Description = "Segundo piso", Status = "maintenance" });
    }

    if (!await context.RoomCategories.AnyAsync())
    {
        await context.RoomCategories.AddRangeAsync(
            new RoomCategory
            {
                Name = "Standard",
                Description = "Habitacion comoda para estancias cortas.",
                MaxCapacity = 2,
                Amenities = "WiFi, TV, Bano privado"
            },
            new RoomCategory
            {
                Name = "Suite",
                Description = "Habitacion amplia para familias o ejecutivos.",
                MaxCapacity = 4,
                Amenities = "WiFi, TV, Jacuzzi, Minibar"
            });
    }

    await context.SaveChangesAsync();
}

app.MapControllers();

app.Run();
