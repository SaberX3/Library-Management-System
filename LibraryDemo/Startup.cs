﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDemo.Data;
using LibraryDemo.Infrastructure;
using LibraryDemo.Models.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<LendingInfoDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LendingInfoDbContext"));
            });
            services.AddDbContext<StudentIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StudentIdentityDbContext"));
            });
            services.AddIdentity<Student, IdentityRole>(opts =>
            {

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<StudentIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.HttpOnly = true;
                opts.LoginPath = "/StudentAccount/Login";
                opts.AccessDeniedPath = "/StudentAccount/Login";
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
            services.AddSingleton<EmailSender>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=BookInfo}/{action=Index}");
            });
            StudentInitiator.InitialStudents(app.ApplicationServices).Wait();
            BookInitiator.BookInitial(app.ApplicationServices).Wait();
        }
    }
}
