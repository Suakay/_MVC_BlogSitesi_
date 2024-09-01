using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.DTOs.AuthorDTOs
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }    
        public string FirstName {  get; set; }  
        public string LastName { get; set; }    
        public string Email {  get; set; }  
        public byte[]? Image { get; set; }  
    }
}
