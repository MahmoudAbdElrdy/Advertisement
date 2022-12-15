using Common;
using Common.Infrastructures;
using System.Collections.Generic;

namespace AuthDomain.Entities {
  public class ServiceType : BaseEntity<string> {
        public ServiceType()
        {
            Services = new HashSet<Service>(); 
        }
        public LocalizedData Name { get; set; }
        public LocalizedData Description { get; set; }  
        public ICollection<Service> Services { get; set; }

    }
}