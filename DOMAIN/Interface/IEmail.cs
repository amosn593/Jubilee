using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interface;
public interface IEmail
{
    Task<string> SendEmail(string toEmail, string subject, string body);
}
