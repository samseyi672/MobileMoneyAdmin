using AccountAndTransactions.genericobj;
using AccountAndTransactions.IServices;
using AccountAndTransactions.Services;
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
using Steeltoe.Discovery.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountAndTransactions
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
            services.AddDiscoveryClient(Configuration);
            services.AddHttpClient();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            var appSetting = Configuration.GetSection("AppSettingConfig");
            services.Configure<AppSettings>(appSetting);
            var jwtSetting = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSetting);
            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.SaveToken = true;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = jwtSettings.Issuer,
                 ValidAudience = jwtSettings.Audience,
                 IssuerSigningKey = new SymmetricSecurityKey(key)
             };
         });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:5173")
                        .SetIsOriginAllowed((host) => "http://localhost:5173".Equals(host, StringComparison.InvariantCultureIgnoreCase))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                         );
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobile Money Admin Office API", Version = "v1" });
                //  c.OperationFilter<SwaggerFileOperationFilter>();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                //new List<string>()
                new string[] {}
                   }
                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.SchemaFilter<SwaggerIgnoreFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                services.AddControllers();
            });
      }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDiscoveryClient();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            // Add your custom exception handling middleware
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRouting();
            //app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();

            app.UseAuthorization();
 
            //allow access to static files in security 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Mobile Money Admin Back office Management Office v1");
            });
        }
    }
}
