using LoginAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LoginAPI;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Method to configure services
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    // Add additional services and configurations here

    // Build the configuration
    var configuration = builder.Configuration;

    // Configure the database context and enable dependency injection
    services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
}