using AuthDomain.Entities.Auth;
using System.Collections.Generic;

namespace AuthDomain.Entities.Ads {
  public class Client : User {
    public HashSet<SpaceInfo> SpaceInfos { get; set; } = new HashSet<SpaceInfo>();
    public HashSet<ClientDocument> Documents { get; set; } = new HashSet<ClientDocument>();
    public HashSet<AuctionSubiscriber> AuctionSubiscribers { get; set; } = new HashSet<AuctionSubiscriber>();
    public HashSet<AdComplaint> AdComplaints { get; set; } = new HashSet<AdComplaint>();
    public HashSet<OrderComplaint> OrderComplaints { get; set; } = new HashSet<OrderComplaint>();        
    }

}
