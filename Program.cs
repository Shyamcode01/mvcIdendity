using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using dummyIdentity.Areas.Identity.Data;
namespace dummyIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                  

                                    builder.Services.AddDbContext<dbContextsimple>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

                                                builder.Services.AddDefaultIdentity<appIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<dbContextsimple>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}