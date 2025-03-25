using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interface
{
    public interface ISmileIdService
    {
        void WriteObjectToFile2(object obj);
        object ReadObjectFromFile(string smileJobId);
    }
}
