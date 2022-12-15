using AuthDomain.Enums;
using Common;

namespace AuthDomain.Entities.Ads
{
    public class AdStatus : BaseEntityAudit<string>
    {
        public string Comment { get; set; }
        public string AdId { get; set; }
        public AdStatusEnum Status { get; set; }
        public Ad Ad { get; set; }
    }

}
