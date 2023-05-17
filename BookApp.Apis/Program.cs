
using BookApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace BookApp.Apis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration; // Retrieve the configuration object

            // Add services to the container.
            AddDependencyInjectionContainerForBookApp(builder.Services, configuration);


            //https://localhost:44405/
            #region CORS
            //[CORS][1] CORS
            //[CORS][1][1] Allow all
            builder.Services.AddCors(options =>
            {
                //[A] [EnableCors] 
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                //[B] [EnableCors("AllowAnyOrigin")] 
                options.AddPolicy("AllowAnyOrigin", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
        /// <summary>
        /// BookApp related dependancy injection
        /// </summary>
        /// <param name="services"></param>
        private static void AddDependencyInjectionContainerForBookApp(IServiceCollection services, IConfiguration configuration)
        {
            // BookAppDbContext.cs Inject: New DbContext Add
            services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // IBookRepository.cs Inject: add Service(Repository) DI Container
            services.AddTransient<IBookRepository, BookRepository>();
        }
    }
}