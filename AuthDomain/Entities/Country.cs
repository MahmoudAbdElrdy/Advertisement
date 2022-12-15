using Common;
using Common.Infrastructures;
using System.Collections.Generic;

namespace AuthDomain.Entities {
  public class Country : BaseEntity<string> {
        public Country()
        {
            Cities = new HashSet<City>(); 
        }
        public LocalizedData Name { get; set; }
    public ICollection<City> Cities { get; set; }

    }
}