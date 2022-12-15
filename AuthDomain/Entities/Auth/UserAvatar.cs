namespace AuthDomain.Entities.Auth {
  public class UserAvatar:Image {
    public string UserId { get; set; }
    public User User { get; set; }
  }

}