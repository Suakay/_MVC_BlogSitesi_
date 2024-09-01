using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_UI.Areas.Author.Models.ArticleVMs
{
    public class AuthorArticleCreateVM
    {
        public string Title { get; set; }   
        public string Content { get; set; }
        public SelectList? Tags { get; set; }   
        public IFormFile? NewImag {  get; set; }    
        public Guid? TagId { get; set; }    
        public Guid? AuthorId { get; set; } 
    }
}
