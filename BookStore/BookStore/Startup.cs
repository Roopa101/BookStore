using BusinessLayer.Services;
using BusinessLayer.UserInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           
            services.AddSwaggerGen(swagger =>
            {

                // To Enable authorization using Swagger (JWT)  
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using bearer scheme",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                swagger.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                         jwtSecurityScheme, Array.Empty<string>()
                        }
                    });
            });


            // Adding Jwt Bearer
            services.AddTransient<IUserBl, UserBL>();
            services.AddTransient<IUserRL, UserRL>();
            services.AddTransient<IBookBL, BookBL>();
            services.AddTransient<IBookRL, BookRL>();
            services.AddTransient<ICartBL, CartBL>();
            services.AddTransient<ICartRL, CartRL>();
            services.AddTransient<IWishListBL, WishListBL>();
            services.AddTransient<IWishListRL, WishListRL>();
            services.AddTransient<IAddressBL, AddressBL>();
            services.AddTransient<IAddressRL, AddressRL>();



            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN")),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore"));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
