
using BEFESkeleton.Data;
using BEFESkeleton.Tools;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BEFESkeleton
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SkeletonDbContext>(opt =>
            {
                opt
                .UseSqlite(builder.Configuration["db:conn"])
                .UseLazyLoadingProxies();
            });


            builder.Services.AddAutoMapper(cfg =>
            {

            }, typeof(DtoProfile));

            builder.Services.AddControllers(cfg =>
            {
                cfg.Filters.Add<ExceptionFilter>();
                cfg.Filters.Add<ValidationFilter>();
            });

            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddHangfire(config =>
                    config.UseSQLiteStorage(builder.Configuration["db:conn_hangfire"]));
            builder.Services.AddHangfireServer();

            builder.Services.AddSignalR();


            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapHub<SignalRHub>("/hub");

            app.UseHangfireDashboard();

            app.MapControllers();

            app.Run();
        }
    }
}
