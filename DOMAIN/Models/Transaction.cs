using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public DateTime TransDate { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; } = 0;
    public string? Status { get; set; } = "Complete";
    public long? PhoneNumber { get; set; }
    public string? Type { get; set; } = "Deposit"; // Withdraw
    public int? UserId { get; set; }
    public User? user { get; set; }
}
