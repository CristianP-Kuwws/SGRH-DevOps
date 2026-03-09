using Microsoft.AspNetCore.Builder;
using SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Infrastructure.Persistence.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

//implementar swagger
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

if(builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "SGHR API",
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // AGREGA ESTAS DOS LÍNEAS:
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SGHR API v1");
        options.RoutePrefix = string.Empty; // Esto hará que Swagger sea la página de inicio
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
