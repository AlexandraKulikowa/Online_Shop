using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;
using Serilog;


namespace OnlineShopWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("OnlineShopKulikowa");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));


            services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
            services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();
            services.AddTransient<ICompareRepository, ComparesDbRepository>();
            services.AddTransient<IFavouriteRepository, FavouritesDbRepository>();
            services.AddTransient<IOrderRepository, OrdersDbRepository>();
            services.AddTransient<IBasketsRepository, BasketsDbRepository>();
            services.AddTransient<IProductsRepository, ProductsDbRepository>();
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}