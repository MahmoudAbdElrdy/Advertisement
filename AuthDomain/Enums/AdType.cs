using System.Runtime.Serialization;

namespace AuthDomain.Enums {
  public enum AdType {
        [EnumMember(Value = "Rent")]
        Rent,
        [EnumMember(Value = "Auction")]
        Auction
  }

}
