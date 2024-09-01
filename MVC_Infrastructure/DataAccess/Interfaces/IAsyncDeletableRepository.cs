using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncDeletableRepository<TEntity> where TEntity : BaseEntity
    {
        Task DeleteAsync(TEntity entity); //silmenin herhangi bir dönüşü olmasına gerek yok.
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
