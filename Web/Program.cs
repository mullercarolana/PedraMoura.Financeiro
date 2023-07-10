using ApplicationCore.Entities.Clients;
using ApplicationCore.Entities.Orders;
using ApplicationCore.Entities.Products;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.GetValue<string>("Env");
builder.Configuration.AddEnvironmentVariables();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FinancialDbContext>(a => a.UseSqlServer(connectionString));
builder.WebHost.ConfigureKestrel(options =>
{
    var port = builder.Configuration.GetValue("Port", 80);
    options.Listen(IPAddress.Any, port, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
builder.Services.AddMediatR(m =>
{
    var assemblies = new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.Load("ApplicationCore") };
    m.RegisterServicesFromAssemblies(assemblies: assemblies);
});
builder.Services.AddScoped<IHttpRequestAuthenticateService, HttpRequestAuthenticateService>();
builder.Services.AddScoped<IHttpRequestPixService, HttpRequestPixService>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddTransient<IClientsRepository, ClientsRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});
app.Run();
