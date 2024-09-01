using MVC_Domain.Entities;
using MVC_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.Repositories.ArticleRepositories
{
    public interface IArticleRepository : IRepository, IAsyncInsertableRepository<Article>, IAsyncFindableRepository<Article>, IAsyncOrderableRepository<Article>, IAsyncQueryableRepository<Article>, IAsyncRepository, IAsyncUpdatableRepository<Article>, IAsyncDeletableRepository<Article>
    {
    }
}
