using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Dtos;
public class TransactionDto
{
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; } = 0;
    public long? PhoneNumber { get; set; }
    public int? UserId { get; set; }
}
