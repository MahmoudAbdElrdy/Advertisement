using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using Common;

namespace AuthDomain.Entities {
  public class Transaction :BaseEntityAudit<string> {
    public string RefNo { get; set; }
    public decimal Amount { get; set; }
    public string UserId { get; set; }
    public string AdId { get; set; }
    public string ServiceId { get; set; }
    public TranType TranType { get; set; }
    public TranDir Dir { get; set; }
    public User User { get; set; }
    public Ad Ad { get; set; }
  }
}