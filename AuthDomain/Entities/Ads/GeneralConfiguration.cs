using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using Common;

namespace AuthDomain.Entities.Ads
{
    public class GeneralConfiguration : BaseEntity<long>
    {       
        public ValueTypeEnum ValueType { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }       
}
