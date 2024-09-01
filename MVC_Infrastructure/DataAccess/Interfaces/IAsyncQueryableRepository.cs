using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncQueryableRepository<TEntity> where TEntity : BaseEntity  //Findable'de GetAsync tekli getirirken burda coklu getirir.
                                                                                    //Queryable metodu bu işe yarar.
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true); //ya hepsini getir
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> Expression, bool tracking = true); //ya da koşuluma göre getir.
    }
}
