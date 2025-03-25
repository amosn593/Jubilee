using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models;

namespace DOMAIN.Interface;

public interface ISmileIdService
{
    void WriteObjectToFile(SmileIDCallBackResponse obj);
    object ReadObjectFromFile(string smileJobId);
}
