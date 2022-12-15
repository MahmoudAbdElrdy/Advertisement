using System.Runtime.Serialization;

namespace AuthDomain.Enums {
  public enum AdCategoryEnum {
        [EnumMember(Value = "Fixed")]
        Fixed,
        [EnumMember(Value = "movable")]
        movable,
        [EnumMember(Value = "Digital")]
        Digital,
        [EnumMember(Value = "SocialMedia")]
        SocialMedia
    }

}
