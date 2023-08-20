using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service;
using Repository;
using System;
using Repository.repository;
using WebApiService.Service.dogservice;
using WebApiService.Service.ownerservice;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiService.Service.authenservice;

namespace DogManagement
{

    public class Program
    {
        private readonly static String jwtSecretKey = "khoi@12";
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AddDI(builder.Services);

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

        //adding design pattern such as Singleton

        public static void AddDI(IServiceCollection services)
        {

            // DogSlot01AmazingContext _dBContext = new DogSlot01AmazingContext();

            services.AddDbContext<DogSlot01AmazingContext>(options =>
        options.UseSqlServer("server =DESKTOP-FMGJ86C\\KHOINGO; database=amazingTrainning;uid=sa;pwd=12345; TrustServerCertificate=True"
        ));

            services.AddScoped<DogRepository>();
            //services.AddScoped<IDogService, DogServiceImpl>();
            // services.AddSingleton<IDogService, DogServiceImpl>();
            services.AddScoped<IDogService>(provider =>
 {
     var mapper = provider.GetRequiredService<IMapper>();
     var dogRepository = provider.GetRequiredService<DogRepository>();
     return new DogServiceImpl(mapper, dogRepository);
 });

            services.AddScoped<UserRepo>();
            services.AddScoped<AuthenService>();
            

            //other entity ...
            // services.AddScoped<OwnerRepository>();
            // services.AddScoped<IOwnerService, OwnerServiceImpl>();



            //add sth relating to the migration 


            //adding jwt token configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = "http://localhost:5293",
                                ValidAudience = "http://localhost:5293",
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))

                            };
                        }
                        );

            // services.AddAutoMapper(typeof(Program).Assembly);
            services.AddAutoMapper(typeof(Program).Assembly);




        }



    }
}