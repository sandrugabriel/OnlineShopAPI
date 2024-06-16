using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Customers.Repository;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.Data;
using OnlineShop.Options.Repository;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.Options.Service;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.OrderDetailDetails.Repository;
using OnlineShop.OrderDetailDetails.Service;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.OrderDetails.Service.interfaces;
using OnlineShop.Orders.Repository;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Orders.Service;
using OnlineShop.Orders.Service.interfaces;
using OnlineShop.ProductOptions.Repository;
using OnlineShop.ProductOptions.Repository.interfaces;
using OnlineShop.ProductOptions.Service.interfaces;
using OnlineShop.ProductProductOptions.Service;
using OnlineShop.Products.Repository;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.Products.Service;
using OnlineShop.Products.Service.interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        builder.Services.AddScoped<IQueryServiceProduct, QueryServiceProduct>();
        builder.Services.AddScoped<ICommandServiceProduct, CommandServiceProduct>();

        builder.Services.AddScoped<IRepositoryOrder, RepositoryOrder>();
        builder.Services.AddScoped<IQueryServiceOrder, QueryServiceOrder>();

        builder.Services.AddScoped<IRepositoryProductOption, RepositoryProductOption>();
        builder.Services.AddScoped<IQueryServiceProductOption, QueryServiceProductOption>();

        builder.Services.AddScoped<IRepositoryOrderDetail, RepositoryOrderDetail>();
        builder.Services.AddScoped<IQueryServiceOrderDetail, QueryServiceOrderDetail>();

        builder.Services.AddScoped<IRepositoryCustomer, RepositoryCustomers>();
        builder.Services.AddScoped<IQueryServiceCustomer, QueryServiceCustomer>();
        builder.Services.AddScoped<ICommandServiceCustomer, CommandServiceCustomer>();

        builder.Services.AddScoped<IRepositoryOption, RepositoryOption>();
        builder.Services.AddScoped<IQueryServiceOption, QueryServiceOption>();
        builder.Services.AddScoped<ICommandServiceOption, CommandServiceOption>();


        builder.Services.AddDbContext<AppDbContext>(op => op.UseMySql(builder.Configuration.GetConnectionString("Default")!,
            new MySqlServerVersion(new Version(8, 0, 21))), ServiceLifetime.Scoped);

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
            .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        app.Run();
    }
}