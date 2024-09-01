using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncFindableRepository<TEntity> where TEntity : BaseEntity  //anlam itibariye bulunabilir demek
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null); //bool dönecek,AnyAsync metodunun sartlari bu sekilde.
                                                                                //gelirse al,gelmezse null dönecek.
                                                                                //Any metodu DbSet metotlarından biridir.Any metodunu parametresiz kullanırsak hangi DbSete kullanıyosak o metodu
                                                                                //o DbSetin herhangi bir nesnesi var mı ona göre true false dönüyor.
                                                                                //Ama içeriye bir expression verirsek ona göre o sartlara göre true false donüyor.
                                                                                //Expression null ise parametresiz Any calısır. Expression null değilse parametreli Any calısır.
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true); //tracking=>degisiklik takibine girsin mi girmesin mi. True verirsem takipli
                                                                    //false verirsem takipsiz. Daha cok takipli kullanıyoruz.
                                                                    //Id ile cagıracaksam GetById. Id'ye göre getir.
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true); //Id dısında mesela email ile cagıracaksam
                                                                                                   //GetAsync metodu ile çagırırım.
                                                                                                   //Entity dondugu için tracking i dahil ediyoruz.

    }
}
