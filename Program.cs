using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using dummyIdentity.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using dummyIdentity.Repository.service;
using dummyIdentity.Repository;
using Microsoft.Extensions.DependencyInjection;
namespace dummyIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                  

            // database connection 
                     builder.Services.AddDbContext<dbContextsimple>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

            // identity 
         builder.Services.AddDefaultIdentity<appIdentityUser>(options => 
         options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<dbContextsimple>();


            // validation 
            builder.Services.AddControllersWithViews().AddMvcOptions(option =>
            {
                option.EnableEndpointRouting = false;
            }).AddDataAnnotationsLocalization();


            // use dependency email
  
            builder.Services.AddTransient<EmailService>();
 

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
        
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}