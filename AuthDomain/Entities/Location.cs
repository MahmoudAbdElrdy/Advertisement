using Common;

namespace AuthDomain.Entities.Ads {
  public class Location:BaseEntity<string> {
    public decimal? Lat { get; set; }
    public decimal? Lng { get; set; }
  }

}
