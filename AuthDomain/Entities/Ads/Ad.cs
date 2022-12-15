using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads
{

    public class Ad : BaseEntityAuditWithDelete<string>
    { 
        public int duration { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public AdType AdType { get; set; }
        public AdCategoryEnum AdCategory { get; set; }
        public decimal Price { get; set; }
        public decimal? Availability { get; set; }
        public string SpaceInfoId { get; set; }
        public SpaceInfo SpaceInfo { get; set; }
        public HashSet<AdStatus> AdStatuses { get; set; } = new HashSet<AdStatus>();
        public HashSet<Auction> Auctions { get; set; } = new HashSet<Auction>();
        public HashSet<AdClient> AdClients { get; set; } = new HashSet<AdClient>();
        public HashSet<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public HashSet<FreeService> FreeServices { get; set; } = new HashSet<FreeService>();
        //public HashSet<PaidService> PaidServices { get; set; } = new HashSet<PaidService>();
        public HashSet<AdInterval> AdIntervals { get; set; } = new HashSet<AdInterval>();
        public HashSet<AdFavourite> AdFavourites { get; set; } = new HashSet<AdFavourite>();
        public HashSet<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public HashSet<AdComplaint> AdComplaints { get; set; } = new HashSet<AdComplaint>();
    }

}
