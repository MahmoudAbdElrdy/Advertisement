namespace AuthDomain.Entities.Ads {
  public class ClientDocument : Image {
    public bool Rejected { get; set; }

    public string ClientId { get; set; }
    public Client Client { get; set; }
  }

}
