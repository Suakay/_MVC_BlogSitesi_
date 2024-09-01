using MVC_Domain.Entities;
using MVC_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.Repositories.AuthourRepositories
{
    public interface IAuthorRepositori : IRepository, IAsyncInsertableRepository<Authour>, IAsyncFindableRepository<Authour>, IAsyncOrderableRepository<Authour>, IAsyncQueryableRepository<Authour>, IAsyncRepository, IAsyncUpdatableRepository<Authour>, IAsyncDeletableRepository<Authour>
    {
    }
}
