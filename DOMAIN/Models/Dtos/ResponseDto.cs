using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Dtos;
public class ResponseDto
{
    public string Error { get; set; }
    public bool Success { get; set; }
    public object Result { get; set; }
}
