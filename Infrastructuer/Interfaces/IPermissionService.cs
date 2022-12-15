using System.Collections.Generic;
using System.Threading.Tasks;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;

namespace Infrastructure.Interfaces {
  public interface IPermissionService {
    IEnumerable<string> GetPermissions();
    Task AddPermissionsToRole(Role role, Permission permission);
    Task AddPermissionsToRole(Role role, string permission);
    }
}
