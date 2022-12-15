namespace AuthDomain.Entities.Ads {
  public class SpaceLocation : Location {
    public string SpaceInfoId { get; set; }
    public SpaceInfo  Space{ get; set; }
  }

}
