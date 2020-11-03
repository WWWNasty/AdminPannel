using System.IO;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces;
using Admin.Panel.Core.Services.QuestionaryServices;
using Admin.Panel.Core.Services.QuestionaryServices.QuestionsServices;
using Admin.Panel.Core.Services.UserManageServices;
using Admin.Panel.Data.MapperProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Admin.Panel.Data.Repositories.Questionary;
using Admin.Panel.Web.Servises;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Admin.Panel.Data.Repositories.Questionary.Questions;
using Admin.Panel.Data.Repositories.UserManage;
using Microsoft.Extensions.Logging;


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
            services.AddScoped<IQuestionaryService, QuestionaryService>();
            services.AddScoped<IQuestionaryRepository, QuestionaryRepository>();
            services.AddScoped<ISelectableAnswersListRepository, SelectableAnswersListRepository>();
            services.AddScoped<IAnswersService,AnswersService>();
            services.AddScoped<IQuestionaryInputFieldTypesRepository, QuestionaryInputFieldsTypesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserStore<User>, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRoleStore<ApplicationRole>, RoleRepository>();
            services.AddIdentity<User, ApplicationRole>()
                //.AddDapperStores(options => {
                //    options.AddRolesTable<ExtendedRolesTable, ExtendedIdentityRole>();
                //});
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            //TODO сложность пароля сделать по сложнее
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();  
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
         
            
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
