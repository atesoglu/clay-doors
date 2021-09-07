using System;
using System.Text;
using API.HealthCheck;
using API.Middlewares;
using Application.Models.Authentication;
using Application.Persistence;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastucture(Configuration);

            services.AddHealthChecks();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fc58ff8e-bac4-4dfb-94b6-59c3cbc226cb")),
                    };
                });


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDataContext dbContext)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsProduction()) app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health-checks", HealthChecksBuilder.CreateOptions());

                if (env.IsDevelopment())
                {
                    endpoints.MapGet("/debug-config", ctx => ctx.Response.WriteAsync((Configuration as IConfigurationRoot).GetDebugView()));
                }
            });

            using var scope = app.ApplicationServices.CreateScope();
            scope.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }
    }
}