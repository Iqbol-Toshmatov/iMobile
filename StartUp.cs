using iMobile.DAL;
using iMobile.DAL.Interfaces;
using iMobile.DAL.Repositories;
using iMobile.Service.Implementations;
using iMobile.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iMobile
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllersWithViews();

            var connection = Configuration.GetConnectionString("DafaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connection));

            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IPhoneService, PhoneService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
