using BookApp.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();

        // Get the IConfiguration instance
        var configuration = builder.Configuration;


        AddDependencyInjectionContainerForBookApp(builder.Services, configuration);


        var app = builder.Build();
        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();

        
    }
    private static void AddDependencyInjectionContainerForBookApp(IServiceCollection services, IConfiguration configuration)
    {
        // BookAppDbContext.cs Inject: New DbContext Add
        services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // IBookRepository.cs Inject: add Service(Repository) DI Container
        services.AddTransient<IBookRepository, BookRepository>();
    }
}