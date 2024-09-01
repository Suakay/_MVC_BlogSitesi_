using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncInsertableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity); //AddAsync tek bir entity gönderirsek ekleyecek. Bunun dönüşü TEntity(<> içindeki)
        Task AddRangeAsync(IEnumerable<TEntity> entities); //AddRangeAsync de ilgili entity tipinde(TEntity) bir collection(IEnumerable) 
                                                           //gönderirsek o collection'ın hepsini aynı anda ekleyecek.
                                                           //Bunun dönüşü yok.(Void demek aslında. Task asenkronun voidi diyebiliriz.)
    }
}
