using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.MailServices
{
    public interface IMailService
    {
        Task SendMailAsync(string mail, string subject, string message);
    }
}
