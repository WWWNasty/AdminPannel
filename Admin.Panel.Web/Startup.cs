using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;
using Admin.Panel.Core.Services;
using Admin.Panel.Data.MapperProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Admin.Panel.Data.Repositories;
using Admin.Panel.Data.Repositories.Questionary;
using Admin.Panel.Web.Servises;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using Admin.Panel.Data.Repositories.Questionary;


namespace Admin.Panel.Web
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
            services.AddScoped<IManageUserRepository, ManageUserRepository>();
            services.AddScoped<IManageUserService, ManageUserService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IObjectPropertiesRepository, ObjectPropertiesRepository>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IQuestionaryObjectRepository, QuestionaryObjectRepository>();
            services.AddScoped<IQuestionaryObjectService, QuestionaryObjectService>();
            services.AddScoped<IQuestionaryObjectTypesRepository, QuestionaryObjectTypesRepository>();
            services.AddScoped<IQuestionaryObjectTypesService, QuestionaryObjectTypesService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserStore<User>, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRoleStore<ApplicationRole>, RoleRepository>();
            services.AddIdentity<User, ApplicationRole>()
                //.AddDapperStores(options => {
                //    options.AddRolesTable<ExtendedRolesTable, ExtendedIdentityRole>();
                //});
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            //TODO ��������� ������ ��������� true �� �����
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
            //services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            //{
            //    var libraryPath = Path.GetFullPath(
            //        Path.Combine(HostEnvironment.ContentRootPath, "..", "MyClassLib"));
            //    options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
