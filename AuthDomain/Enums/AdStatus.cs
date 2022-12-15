using System.Runtime.Serialization;

namespace AuthDomain.Enums {
  public enum AdStatusEnum
    {
        [EnumMember(Value = "Open")]
        Open,  
        [EnumMember(Value = "Closed")]
        Closed,
        [EnumMember(Value = "Reserved")]
        Reserved
    }

}
