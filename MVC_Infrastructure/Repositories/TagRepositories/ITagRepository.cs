
using MVC_Domain.Entities;
using MVC_Infrastructure.DataAccess.Interfaces;

namespace BlogMainStructure.Infrastructure.Repositories.TagRepositories
{
 
    public interface ITagRepository : IRepository, IAsyncInsertableRepository<Tag>, IAsyncFindableRepository<Tag>, IAsyncOrderableRepository<Tag>, IAsyncQueryableRepository<Tag>, IAsyncRepository, IAsyncUpdatableRepository<Tag>, IAsyncDeletableRepository<Tag>
    {
    }
}
