using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;

public class SmileIDCallBackResponse
{
    public SmileIDActions? Actions { get; set; }
    public string? Country { get; set; }
    public string? DOB { get; set; }
    public string? Document { get; set; }
    public string? ExpirationDate { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public string? IDNumber { get; set; }
    public string? IDType { get; set; }
    public SmileIDImageLinks? ImageLinks { get; set; }
    public string? IssuanceDate { get; set; }
    public string? KYCReceipt { get; set; }
    public string? PhoneNumber2 { get; set; }
    public string? SecondaryIDNumber { get; set; }
    public string? signature { get; set; }
    public string? timestamp { get; set; }
    public string? ResultCode { get; set; }
    public string? ResultText { get; set; }
    public string? ResultType { get; set; }
    public string? SmileJobID { get; set; }
    public string? IsFinalResult { get; set; }
    public SmileIDPartnerParams? PartnerParams { get; set; }

}

public class SmileIDImageLinks
{
    public string? id_card_back { get; set; }
    public string? id_card_image { get; set; }
    public string? selfie_image { get; set; }
}

public class SmileIDPartnerParams
{
    public string? job_id { get; set; }
    public string? job_type { get; set; }
    public string? user_id { get; set; }
}

public class SmileIDActions
{
    public string? Document_Check { get; set; }
    public string? Liveness_Check { get; set; }
    public string? Register_Selfie { get; set; }
    public string? Verify_Document { get; set; }
    public string? Human_Review_Compare { get; set; }
    public string? Return_Personal_Info { get; set; }
    public string? Selfie_To_ID_Card_Compare { get; set; }
    public string? Human_Review_Document_Check { get; set; }
    public string? Human_Review_Liveness_Check { get; set; }
}
