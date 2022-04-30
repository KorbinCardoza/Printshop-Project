using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using PrintShop.Extensions;
using PrintShop.Identity;
using PrintShop.Models;
using PrintShop.Models.Entity;
using Serilog;
using PrintShop.Services;

namespace PrintShop
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
          

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Enable sensitive logging in dev
            if (env == "Development")
            {
                services.AddDbContextPool<PrintshopContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("PrintShopDev"),
                    // Split queries with includes for performance
                    o =>
                    {
                        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }).EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContextPool<PrintshopContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("PrintShopProd"),
                    // Split queries with includes for performance
                    o =>
                    {
                        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
            }
            services.AddControllersWithViews(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole("User", "Admin")
                    .Build();
            });

            services.AddHttpContextAccessor();
            services.AddTransient<AdUserInfo>();
            services.AddScoped<IClaimsTransformation, RolesToClaim>();
            services.AddScoped<ICopyCenterServices, CopyCenterServices>();
            services.AddRazorPages()
        .AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/StatusError", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Adds certain properties about the user to each log
            app.UseMiddleware<LogUserMiddleware>();
            // Use Serilog's less noisy request logging over microsoft's
            // must be declared before the UseEndpoints
            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                

            });
        }
    }
}
