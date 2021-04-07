using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.Completed;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces;
using Admin.Panel.Core.Services.QuestionaryServices;
using Admin.Panel.Core.Services.QuestionaryServices.Completed;
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
using Admin.Panel.Data.Repositories.Questionary.Completed;
using Admin.Panel.Web.Servises;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Admin.Panel.Data.Repositories.Questionary.Questions;
using Admin.Panel.Data.Repositories.UserManage;
using Admin.Panel.Web.Extensions;
using Admin.Panel.Web.Logging;
using Microsoft.AspNetCore.Http;
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
            services.AddScoped<IQuestionaryObjectRepository, QuestionaryObjectRepository>();
            services.AddScoped<IQuestionaryObjectService, QuestionaryObjectService>();
            services.AddScoped<IQuestionaryObjectTypesRepository, QuestionaryObjectTypesRepository>();
            services.AddScoped<IQuestionaryObjectTypesService, QuestionaryObjectTypesService>();
            services.AddScoped<IQuestionaryService, QuestionaryService>();
            services.AddScoped<IQuestionaryRepository, QuestionaryRepository>();
            services.AddScoped<ISelectableAnswersListRepository, SelectableAnswersListRepository>();
            services.AddScoped<IAnswersService, AnswersService>();
            services.AddScoped<IQuestionaryInputFieldTypesRepository, QuestionaryInputFieldsTypesRepository>();
            services.AddScoped<ICompletedQuestionaryRepository, CompletedQuestionaryRepository>();
            services.AddScoped<ICompletedQuestionaryService, CompletedQuestionaryService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserStore<User>, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRoleStore<ApplicationRole>, RoleRepository>();
            services.AddSingleton<UserNameEnricher>();
            services.AddScoped<CustomClaimsCookieSignInHelper<User>>();
            services.AddIdentity<User, ApplicationRole>()
                
                //.AddDapperStores(options => {
                //    options.AddRolesTable<ExtendedRolesTable, ExtendedIdentityRole>();
                //});
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            //TODO сложность пароля сделать по сложнее
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            });
            services.AddControllers();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseMiddleware<RedirectToPasswordResetMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            
        }
    }

    public class RedirectToPasswordResetMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToPasswordResetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Claims.FirstOrDefault(c => c.Type == "needs_reset_password")?.Value == true.ToString() && context.Request.Path != "/Account/ChangePassword" && context.Request.Path != "/Account/Logout")
            {
                var allRouteParts = context.Request.Path.ToString().Split('/');
                var path = allRouteParts.Take(allRouteParts.Length - 2).ToArray();
                var basePath = string.Join("/", path);
                context.Response.Redirect(basePath + "/Account/ChangePassword");
                return;
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}