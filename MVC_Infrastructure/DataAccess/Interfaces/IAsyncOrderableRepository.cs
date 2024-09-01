using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncOrderableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true);
        //TKey orderBy ın yazım şekli içinde var. Sıralama için.
        //ilk kosul orderBy da verdiğim propertye göre sırala,ikinci ters mi istiyorum ona göre sırala,son kosuş tracking olsun mu olmasın mı
        //ona göre sırala.
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true);
        //il kosul yas>=18 olanları getir,ikinci Name e göre sırala,üçücü de false verirsem tersten sıralama istemiyorum true verirsem A dan Zye degil
        //Zden Aya sıralar,sonuncu tracking olsun mu olmasın mı
    }
}
