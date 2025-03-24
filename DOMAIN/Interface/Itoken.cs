using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models;

namespace DOMAIN.Interface;
public interface Itoken
{
    string GenerateToken(User appUser);
}
