using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using Common;

namespace AuthDomain.Entities.Ads
{
    public class OrderComplaint : BaseEntityAudit<string>
    {
        public string ComplaintReason { get; set; }
        public string ComplaintReasonReplay { get; set; }
        public bool? IsComplaintSeen { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }      
        public string OrderId { get; set; }
        public ComplainType ComplainType { get; set; }

        public AdInterval AdInterval { get; set; }
    }
}
