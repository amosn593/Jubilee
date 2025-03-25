using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;
public class Bank
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public bool Active { get; set; }
    public List<Branch> Branches { get; set; } = [];
}

public class Branch
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public bool Active { get; set; }
}
