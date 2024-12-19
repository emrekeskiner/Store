using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("mssqlconnection"),
                    x => x.MigrationsAssembly("StoreApp"));

                    options.EnableSensitiveDataLogging(true);
                });
        }

         public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {   //e mail onaylanacak mı ? Kişi mailini onaylamadığı sürece oturum açamaz (true)
                options.SignIn.RequireConfirmedAccount = false;
                //mail adresi ile kişiler eşleştirilsin mi
                options.User.RequireUniqueEmail = true;
                //şifre de büyük harf olacak mı ?
                options.Password.RequireUppercase = false;
                // şifrede küçük harf olacak mı ?
                options.Password.RequireLowercase = false;
                //Rakam gereksin mi ?
                options.Password.RequireDigit = false;
                // şifrenin uzunluğu
                options.Password.RequiredLength = 6;

                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<RepositoryContext>();
        }
        public static void ConfigureSession(this IServiceCollection services)
        {
            //Session servisleri inşaası
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            //Sessionu okumak için accessor nesnesi
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            //IoC Invesion of Control ün Register i
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IAuthService, AuthManager>();
        }

        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
        }
        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options => 
            {
                options.LowercaseUrls=true;
                options.AppendTrailingSlash=false;
                
            });
        }


    }
}