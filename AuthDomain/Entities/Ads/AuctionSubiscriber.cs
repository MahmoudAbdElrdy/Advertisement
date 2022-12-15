using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads {

  public class AuctionSubiscriber : BaseEntityAuditWithDelete<string> {
 
    public string ClientId { get; set; }
    public string AuctionId { get; set; }
    public Decimal Price { get; set; }
    public AuctionStatus AuctionStatus { get; set; }
    public int AuctionDays { get; set; }
    public Auction Auction { get; set; }
    public Client Client{ get; set; }
   }

}
