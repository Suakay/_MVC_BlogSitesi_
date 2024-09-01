using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Entities
{
    public class Article:AuditableEntity
    {
        public string Title { get; set; }   
        public string Content { get; set; } 
        public int ViewCount {  get; set; } 
        public DateTime PublishDate { get; set; }   
        public byte[]? Image { get; set; }  
        public Guid AuthourId { get; set; } 
        public virtual Authour Authour { get; set; }    
        public Guid? TagId { get; set; }    
        public virtual Tag? Tag { get; set; }   
    }
}
