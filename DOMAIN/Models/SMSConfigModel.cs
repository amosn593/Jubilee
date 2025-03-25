using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;
public class SMSConfigModel
{
    [Key]
    public int Id { get; set; }
    public string SMSURL { get; set; }
    public string Code { get; set; }
    public string Apikey { get; set; }
    public string ClientId { get; set; }
    public string SenderId { get; set; }
    public string FromName { get; set; }
    public bool Active { get; set; }
    public SmsTyoe SMsTyoe { get; set; }
    public string? BusinessId { get; set; }
    public string SenderName { get; set; }
    public bool IsDeleted { get; set; }
}

public enum SmsTyoe
{
    tenzi,
    thirdparty
}

