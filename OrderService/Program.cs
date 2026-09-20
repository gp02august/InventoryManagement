using OrderService.Middleware;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repository;
using OrderService.Repository.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var logDirectory = Path.Combine(
    builder.Environment.ContentRootPath,
    "Logs");

Directory.CreateDirectory(logDirectory);

var logFilePath = Path.Combine(
    logDirectory,
    "order-service.log");

builder.Logging.AddProvider(
    new FileLoggerProvider(logFilePath));

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpClient<IProductApiClient, ProductApiClient>(
    (serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var baseUrl = configuration["ProductService:BaseUrl"];

    client.BaseAddress = new Uri(baseUrl!);
});

builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();