
using BookMangementSystemApi.Data;
using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using BookMangementSystemApi.Repository.IMP;
using BookMangementSystemApi.Service;
using BookMangementSystemApi.Service.IMP;
using BookMangementSystemApi.Validation;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
            builder.Services.AddScoped(typeof(IReaderRepository),typeof(ReaderRepository));
            builder.Services.AddScoped(typeof(IAutherRepository),typeof(AutherRepository));


            builder.Services.AddScoped<IBookService, BookService>();
            
            builder.Services.AddScoped<IBorrowService, BorrowService>();
            
            builder.Services.AddScoped<IReaderService, ReaderService>();
            
            builder.Services.AddScoped<IBookValidator, BookValidator>();

            builder.Services.AddScoped<IReaderValidator, ReaderValidator>();

            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddScoped<IAutherService, AutherService>();
            

          


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(a =>
            a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.MapControllers();

            app.Run();
        }
    }
}
