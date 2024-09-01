using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncUpdatableRepository<TEntity> where TEntity : BaseEntity // ,new() // new sartı demek nesnesi üretilen bir baseEntity olmak zorunda.
                                                                                   //Yani abstract class olamaz.Abstract classın nesnesi üretilmez cunkü.
    {
        Task<TEntity> UpdateAsync(TEntity entity); //Donüşü TEntity, UpdateAsync metodu tek bir entity günceller.
    }
}
