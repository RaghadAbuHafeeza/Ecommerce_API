using AutoMapper;
using Ecommerce.API.Mapping_Profiles;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.IRepositories.IServices;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("defaultCash",
                    new CacheProfile()
                    {
                        Duration = 30,
                        Location = ResponseCacheLocation.Any
                    });
            });

            // Configure DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies(false);
            });

            builder.Services.AddResponseCaching();

            // Register Repositories and Services
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });

            var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");

            //builder.services.addscoped(typeof(usermanager<localuser>));
            //builder.services.addscoped(typeof(rolemanager<identityrole>));
            //builder.services.addscoped(typeof(signinmanager<identityrole>));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                };
            });

            // Configure Identity
            builder.Services.AddIdentity<LocalUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Configure API Behavior
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                          .Where(x => x.Value.Errors.Count > 0)
                          .SelectMany(x => x.Value.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();
                    var validationResponse = new ApiValidationResponse(statusCode: 400) { Errors = errors };
                    return new BadRequestObjectResult(validationResponse);
                };
            });

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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
