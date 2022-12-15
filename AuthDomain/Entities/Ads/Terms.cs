using Common;

namespace AuthDomain.Entities.Ads {
  public class Terms : BaseEntity<string> {
    public string Lang { get; set; }
    public string Description { get; set; }
  }

}
