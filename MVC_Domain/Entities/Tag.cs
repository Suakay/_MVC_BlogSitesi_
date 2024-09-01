using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Entities
{
    public class Tag:AuditableEntity
    {
        public Tag()
        {
            Articles = new HashSet<Article>();
        }

        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
