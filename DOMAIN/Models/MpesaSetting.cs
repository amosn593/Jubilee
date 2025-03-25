using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;
public class MpesaSetting
{
    public string? AppName { get; set; }
    public string? ConsumerKey { get; set; }
    public string? ConsumerSecret { get; set; }
    public string? PassKey { get; set; }
    public long? BusinessShortCode { get; set; }
}

public class AccessTokenResponse
{
    public int Id { get; set; }
    public string? access_token { get; set; }
    public int? expires_in { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpireDate { get; set; }
}

public class StkPushModel
{
    public long? BusinessShortCode { get; set; } = null;
    public string? Password { get; set; }
    public string? Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
    public string? TransactionType { get; set; } = "CustomerPayBillOnline";
    public int? Amount { get; set; } = 1;
    public long? PartyA { get; set; } = 0; // 254702240787;
    public long? PartyB { get; set; } = 0; // null;
    public long? PhoneNumber { get; set; } = 254702240787;
    public string? CallBackURL { get; set; } = "https://fpnqnd5g-7299.uks1.devtunnels.ms/api/Mobile/StkPushCallBackUrl";
    public string? AccountReference { get; set; } = "C#API";
    public string? TransactionDesc { get; set; } = "C#API";
}


public class StkPushResponseModel
{
    public string? MerchantRequestID { get; set; }
    public string? CheckoutRequestID { get; set; }
    public string? ResponseCode { get; set; }
    public string? ResponseDescription { get; set; }
    public string? CustomerMessage { get; set; }
}


public class StkResultObject
{
    public Body Body { get; set; }
}

public class Body
{
    public stkCallback stkCallback { get; set; }
}

public class stkCallback
{
    public string MerchantRequestID { get; set; }
    public string CheckoutRequestID { get; set; }
    public int ResultCode { get; set; }
    public string ResultDesc { get; set; }
    public Callbackmetadata CallbackMetadata { get; set; }
}

public class Callbackmetadata
{
    public Item[] Item { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public string Value { get; set; }
}
