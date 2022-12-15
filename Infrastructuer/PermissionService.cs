using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using Common.Attributes;
using Common.Extensions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure {
    public class PermissionService : IPermissionService {
        private readonly IServiceProvider _scope;
        private readonly IAppDbContext _context;

        public PermissionService(IServiceProvider scope, IAppDbContext context) {
            _scope = scope;
            _context = context;
        }
        public IEnumerable<string> GetPermissions() {
            foreach (Permission item in Enum.GetValues(typeof(Permission)))
            {
                yield return item.GetAttribute<DescribePermissionAttribute>().Title;
            }
        }

        public async Task AddPermissionsToRole(Role role, Permission permission) {
            var userManager = _scope.GetService<UserManager<User>>();

            role.Permissions ??= "";

            var moduleAttribute = permission.GetAttribute<DescribePermissionAttribute>();
            if (!role.Permissions.Contains((char)permission))
            {

                role.Permissions += (char)permission;
                if (moduleAttribute != null)
                {
                    SystemModule module = moduleAttribute.Module;
                    var users = await userManager.GetUsersInRoleAsync(role.Name);
                    foreach (var user in users)
                    {
                        if (((user.AllowedModules & module) == 0))
                        {
                            //add user to this module
                            user.AllowedModules |= module;
                        }

                        await userManager.UpdateAsync(user);
                    }
                }
            }

        }

        public async Task AddPermissionsToRole(Role role, string permission) {
            await AddPermissionsToRole(role, ConvertStingToPermisson(permission));

        }
        private Permission ConvertStingToPermisson(string str) {
            var attrs = (typeof(Permission)).GetFields().ToList();
            foreach (var attr in attrs)
            {
                if (attr.GetCustomAttribute<DescribePermissionAttribute>() != null)
                {
                    if (str == attr.GetCustomAttribute<DescribePermissionAttribute>().Key)
                       return (Permission)attr.GetValue((typeof(Permission)));
                }
            }
            return 0;
        }
    }
}
