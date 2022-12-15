using AuthDomain.Entities.Ads;
using Common;
using Common.Infrastructures;

namespace AuthDomain.Entities {
  public class PaidService : BaseEntity<string> {
        public string AdIntervalId { get; set; }
        public string ServiceId { get; set; }
        public Service Service{ get; set; }
        public AdInterval AdInterval { get; set; } 
    }
}