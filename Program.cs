using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(op =>
            op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            builder.Services.AddScoped<IGenresService, GenresService>();
            builder.Services.AddScoped<IMoviesService, MovieService>();

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(
                c=>c.
                AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod()
                );

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}