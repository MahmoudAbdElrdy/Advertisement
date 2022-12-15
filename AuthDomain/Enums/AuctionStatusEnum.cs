using System.Runtime.Serialization;

namespace AuthDomain.Enums
{
    public enum AuctionStatus
    {
        [EnumMember(Value = "pending")]
        pending = 1,
        [EnumMember(Value = "Done")]
        Done = 2,
        [EnumMember(Value = "Canceled")]
        Canceled = 3

    }

}
