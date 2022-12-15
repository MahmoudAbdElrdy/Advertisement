using System.Runtime.Serialization;

namespace AuthDomain.Enums {
  public enum PaymentType
    {
        [EnumMember(Value = "Pay")]
        Pay,
        [EnumMember(Value = "Refund")]
        Refund
  }

}
