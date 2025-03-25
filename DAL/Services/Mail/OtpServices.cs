//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using DOMAIN.Models;

//namespace DAL.Services.Mail;
//public class OtpServices
//{
//    using System;
//using System.Collections.Generic;
//using System.Security.Cryptography;

//public class OtpService
//{
//    private readonly Dictionary<string, OtpData> _otps = new Dictionary<string, OtpData>(); //In memory cache. For production use a database or redis.
//    private readonly int _otpLength = 6;
//    private readonly int _otpExpirationMinutes = 10;

//    public string GenerateOtp(string userIdentifier)
//    {
//        string otp = GenerateRandomOtp(_otpLength);
//        _otps[userIdentifier] = new OtpData
//        {
//            Otp = otp,
//            Expiration = DateTime.UtcNow.AddMinutes(_otpExpirationMinutes)
//        };
//        return otp;
//    }

//    public bool VerifyOtp(string userIdentifier, string userOtp)
//    {
//        if (_otps.TryGetValue(userIdentifier, out OtpData otpData) &&
//            otpData.Expiration > DateTime.UtcNow &&
//            otpData.Otp == userOtp)
//        {
//            _otps.Remove(userIdentifier); 
//            return true;
//        }
//        return false;
//    }

//    private string GenerateRandomOtp(int length)
//    {
//        using (var rng = RandomNumberGenerator.Create())
//        {
//            byte[] bytes = new byte[length];
//            rng.GetBytes(bytes);
//            return string.Join("", Array.ConvertAll(bytes, b => b % 10));
//        }
//    }
//}
//}
