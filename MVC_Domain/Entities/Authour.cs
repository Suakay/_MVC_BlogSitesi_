using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Entities
{
    public class Authour:AuditableEntity
    {
        public Authour()
        {
            Articles =new  HashSet<Article>();
        }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Email {  get; set; }
        public byte[]? Image { get; set; }  
        public virtual ICollection<Article> Articles { get; set; }  
    }
}
