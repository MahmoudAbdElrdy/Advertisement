using AuthDomain.Entities.Ads;
using Common;
using Common.Infrastructures;

namespace AuthDomain.Entities {
  public class FreeService : BaseEntity<string> {
         public string AdId { get; set; }
        public string ServiceTypeId { get; set; } 
        public ServiceType ServiceType { get; set; }
        public Ad Ad { get; set; } 
    }
}