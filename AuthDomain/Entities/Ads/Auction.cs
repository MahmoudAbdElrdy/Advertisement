using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads {

  public class Auction : BaseEntityAuditWithDelete<string> {
 
    public string AdId { get; set; }
    public Decimal? SeriousSubscriptionAmount { get; set; }
    public AuctionStatus AuctionStatus { get; set; }
    public int AuctionDays { get; set; }
    public Ad Ad { get; set; }
    public HashSet<AuctionSubiscriber> AuctionSubiscribers { get; set; } = new HashSet<AuctionSubiscriber>();
        
    }

}
