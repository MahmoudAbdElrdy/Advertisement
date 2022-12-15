using Common;
using System.Collections.Generic;

namespace AuthDomain.Entities.Ads {
  public class SpaceInfo : BaseEntityAuditWithDelete<string> {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public bool Rejected { get; set; }
    public bool IsAuction { get; set; }
    public string CityId { get; set; }
    public string ClientId { get; set; } 
    public SpaceLocation Location { get; set; }
    public City City { get; set; }    
    public Client Client { get; set; }

    public HashSet<SpaceImage> Images { get; set; } = new HashSet<SpaceImage>();
    public HashSet<Ad> Ads { get; set; } = new HashSet<Ad>();
  }

}
