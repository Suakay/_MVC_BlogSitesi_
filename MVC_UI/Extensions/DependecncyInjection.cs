using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using MVC_Infrastructure.AppContext;

namespace MVC_UI.Extensions
{
    public static  class DependecncyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)//Bu, yeni bir metot adıdır. Metodun amacı, hizmetler koleksiyonuna kullanıcı arayüzüyle ilgili servisleri eklemektir.
        {
            services.AddNotyf(options =>
            {
                options.HasRippleEffect= true;//Bildirimlerin üzerine tıklandığında bir dalga (ripple) efekti gösterileceğini belirtir.
                options.DurationInSeconds = 3;//Bildirimin ekranda kalma süresini 3 saniye olarak ayarlar.
                options.IsDismissable= true;//Kullanıcının bildirimi manuel olarak kapatabilmesini sağlar.
                options.Position = NotyfPosition.BottomRight;//Bildirimlerin ekranın sağ alt köşesinde gösterileceğini belirtir.

            });
            services.AddIdentity<IdentityUser, IdentityRole>()//Bu satır, kimlik yönetimi (Identity) sistemini ekler.
                .AddDefaultTokenProviders()//Bu, kimlik doğrulama işlemleri için varsayılan token sağlayıcılarını (örneğin, e-posta doğrulama, iki faktörlü kimlik doğrulama) ekler.
                .AddEntityFrameworkStores<AppDbContext>();// Bu, Entity Framework Core kullanılarak kullanıcı ve rol bilgilerini veritabanında depolamak için gerekli yapılandırmayı yapar. AppDbContext uygulamanın veritabanı bağlamını temsil eder.



            return services;
        }
    }
}
