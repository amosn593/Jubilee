using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Dtos;
public class AfTalkingSmsMessageResponse
{
    public SMSMessageData SMSMessageData { get; set; }

}

public class Recipient
{
    public string cost { get; set; }
    public string messageId { get; set; }
    public int messageParts { get; set; }
    public string number { get; set; }
    public string status { get; set; }
    public int statusCode { get; set; }
}

public class SMSMessageData
{
    public string Message { get; set; }
    public List<Recipient> Recipients { get; set; }
}

