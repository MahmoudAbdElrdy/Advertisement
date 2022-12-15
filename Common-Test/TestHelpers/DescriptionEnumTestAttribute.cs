using System;

namespace Common_Test.TestHelpers {
  public class DescriptionEnumTestAttribute : Attribute {
    public string Desc { get; set; }
    public DescriptionEnumTestAttribute(string desc) {
      Desc = desc;
    }
  }
}
