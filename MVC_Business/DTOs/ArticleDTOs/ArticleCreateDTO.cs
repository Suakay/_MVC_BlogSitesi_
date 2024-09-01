using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.DTOs.ArticleDTOs
{
    public class ArticleCreateDTO
    {
        public string Title {  get; set; }  
        public string Content { get; set; } 
        public int ViewCount {  get; set; } 
        public Guid? TagId { get; set; }    
        public Guid AuthourId { get; set; } 
        public byte[]? Image { get; set; }  
        public DateTime PublishDate { get; set; }   

    }
}
