using Common.Attributes;

namespace AuthDomain.Enums.Roles {
  public enum Permission {

    [DescribePermission(SystemModule.UserModule, "read_user", "read_user")]
    ReadUser = 1,
    [DescribePermission(SystemModule.UserModule, "add_user", "add_user")]
    AddUser ,
    [DescribePermission(SystemModule.UserModule, "edit_user", "edit_user")]
    EditUser,
    [DescribePermission(SystemModule.UserModule, "remove_user", "remove_user")]
    RemoveUser,
  }
}