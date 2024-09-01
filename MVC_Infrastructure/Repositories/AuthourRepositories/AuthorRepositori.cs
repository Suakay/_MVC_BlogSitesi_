using MVC_Domain.Entities;
using MVC_Infrastructure.AppContext;
using MVC_Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.Repositories.AuthourRepositories
{
    public class AuthorRepositori:EFBaseRepository<Authour>, IAuthorRepositori
    {
        public AuthorRepositori(AppDbContext context): base(context) { }    
    }
}
