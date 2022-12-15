using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Attributes {
  public class DescribePermissionAttribute : Attribute {
    public SystemModule Module { get; set; }
    public string Key { get; set; }
    public string Title { get; set; }

    public DescribePermissionAttribute(SystemModule module ,string key,string title ) {
      Title = title;
      Key = key;
      Module = module;
    }
  }

  public enum SystemModule {
    [EnumMember(Value = "Users")]
    UserModule = 1
  }
}
