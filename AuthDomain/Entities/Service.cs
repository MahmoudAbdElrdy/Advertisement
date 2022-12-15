using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using Common;
using Common.Infrastructures;
using System.Collections.Generic;

namespace AuthDomain.Entities {
  public class Service : BaseEntity<string> {
        public decimal Price { get; set; }
        public string  UserId { get; set; }
        public string ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public User User { get; set; } 
        public HashSet<FreeService> FreeServices { get; set; } = new HashSet<FreeService>();
        public HashSet<PaidService> PaidServices { get; set; } = new HashSet<PaidService>();

    }
}