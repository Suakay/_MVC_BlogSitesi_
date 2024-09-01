using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_Infrastructure.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
            });
           
            //ekle entityleri
            //services.AddScoped<IAuthorRepository, AuthorRepository>();
            



            //Seed Data (genelde mig atarken yoruma alırız)

            //AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();

            //GetAwaiter() bir Task veya Task<T> nesnesinin tamamlanmasını beklemek için kullanılan bir
            //TaskAwaiter<T> nesnesini döndürür.GetResult() ise TaskAwaiter<T> nesnesinin tamamlanmış bir Task ın
            //sonucunu dondurmesini saglar.

            return services;
        }
    }
}
