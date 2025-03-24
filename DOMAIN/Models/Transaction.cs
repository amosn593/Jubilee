using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public DateTime TransDate { get; set; }
    public decimal Amount { get; set; } = 0;
    public string? Status { get; set; } = "Complete";
}
