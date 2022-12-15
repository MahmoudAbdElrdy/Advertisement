using Common;
using Common.Infrastructures;

namespace AuthDomain.Entities {
  public class City : BaseEntity<string> {
    public LocalizedData Name { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
  }
}