using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models;
public class User
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdNumber { get; set; }
    public string KRAPin { get; set; }
    public string? Password { get; set; }
    public string? Nationality { get; set; }
    public string? Bob { get; set; }
    public string? PhysicalAddress { get; set; }
    public string? Residency { get; set; }
    public string? Occupation { get; set; }
    public string? AgeBracket { get; set; }
    public string? InvestmentPeriod { get; set; }
    public string? PreviousInvestments { get; set; }
    public string? UnderstandingInvestments { get; set; }
    public string? InvestmentProducts { get; set; }
    public string? SourceOfFunds { get; set; }
    public string? BankName { get; set; }
    public string? BankBranch { get; set; }
    public string? AccountNo { get; set; }
    public string? SwiftCode { get; set; }
    public string? AdditionalDetails { get; set; }
    public List<Beneficiary> Beneficiaries { get; set; } = [];
    public string? IdAttachmentFront { get; set; }
    public string? IdAttachmentBack { get; set; }
    public string? PinAttachment { get; set; }
    public string? PassportPhoto { get; set; }
    public string? BankDocument { get; set; }
    public bool TermsAndCondition { get; set; } = true;

}

public class Beneficiary
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public string? Allocation { get; set; }
}
