namespace AuthDomain.Entities.Ads {
  public class SpaceImage : Image {
    public bool Rejected { get; set; }

    public string SpaceInfoId { get; set; }
    public SpaceInfo Space { get; set; }
  }

}
