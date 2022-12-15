using System;
using AuthDomain.Entities.Auth;
using Common;

namespace AuthDomain.Entities {
  public class UserCode:BaseEntity<string> {
    public int Code { get; set; }
    public string UserId { get; set; }
    public DateTime CreateDate { get; set; }
    public User User { get; set; }
    
  }
}