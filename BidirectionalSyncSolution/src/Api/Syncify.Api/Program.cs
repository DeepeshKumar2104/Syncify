using Syncify.Application.Interfaces;
using Syncify.Application.Services;
using Syncify.Domain.Interface;
using Syncify.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Syncify.Infrastructure.Persistence.AwsDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<KafkaProducer>(options =>
    new KafkaProducer("localhost:9092", "employee-topic"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<AwsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add controllers to the services collection
builder.Services.AddControllers();

// Add Swagger/OpenAPI support for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optionally, add CORS policy if required for external access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development environment
    app.UseSwagger();  // Add Swagger middleware
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Syncify API V1");
        c.RoutePrefix = string.Empty;  // To serve the Swagger UI at the root
    });
}

// Use CORS if needed
app.UseCors("AllowAll");

// Use HTTPS redirection for secure connections
app.UseHttpsRedirection();

// Enable routing and endpoint mapping
app.MapControllers();  // Map API controllers

// Final step: run the application
app.Run();
