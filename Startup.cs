using Ads.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ads.Api.Interfaces;
using Ads.Api.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Ads.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = Configuration.GetConnectionString("LocalDockerDatabase");
            
            services.AddDbContext<AdsDbContext>(options =>
                options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()));

            services.AddCors(options =>
                options.AddPolicy("default", policy =>
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(host => true)
                )
            );
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "News Api",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Ömrüm Baki Temiz",
                        Email = "omrumbakitemiz@icloud.com",
                        Url = "https://github.com/omrumbakitemiz"
                    },
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://github.com/omrumbakitemiz/News-Api"
                    }
                });
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services
                .AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<AdsDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ICompanyService, CompanyService>();
            
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/ads.api-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors("default");
            app.UseHealthChecks("/health");
            
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "News API V1"); });
            
            app.UseMvc();
        }
    }
}