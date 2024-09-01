using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services) //this metodun dönüşünü tanımlıyor.
                                                                                              //Eklendiği servisi parametre olarak otomtik kendisi alıyor.(IServiceCollection)
        {

            
            // ekle
            // services.AddScoped<IAuthorService, AuthorService>();


            return services;
        }
    }
}
