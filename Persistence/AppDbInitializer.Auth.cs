using System;
using System.Collections.Generic;
using System.Linq;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using Common.Infrastructures;

namespace Persistence {
  public partial class AppDbInitializer {
    public void SeedAuthEverything(AppDbContext context) {
      context.Database.EnsureCreated();

      if (!context.Users.Any()) {
        SeedUsers(context);
      }
      SeedRoles(context);



      if (!context.UserRoles.Any()) {
        SeedUserRoles(context);
      }
    }



    public void SeedUsers(AppDbContext context) {
      var users = new[]
      {
                new User()
                {
                    AccessFailedCount = 0,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "Admin@ErpSystem.com",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = ("Admin@ErpSystem.com").ToUpper(),
                    NormalizedUserName = ("Admin").ToUpper(),
                    PasswordHash = "AQAAAAEAACcQAAAAEIsWjNQ5zjWAxoVt9Hr9Z3XUpWtkXhhil17iNtANiIuQnkIGRynUkDy529Cqpk/Epg==",
                    PhoneNumber = "",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false,
                    UserName = "Admin",
                    CreatedDate=DateTime.UtcNow,
                    CreatedBy="System"
                },
            };
      context.Users.AddRange(users);

      context.SaveChanges();
    }
    public void SeedRoles(AppDbContext context) {
      var roles = new List<Role>();
      var dbRoles = context.Roles.ToList();
      foreach (RolesKey item in Enum.GetValues(typeof(RolesKey))) {
        if (!dbRoles.Any(a => a.Name == item.ToString()))
          roles.Add(new Role(item.ToString()) { NormalizedName = item.ToString().ToUpper() });
      }
      context.Roles.AddRange(roles);
      context.SaveChanges();
    }
    public void SeedUserRoles(AppDbContext context) {
      var user = context.Users.FirstOrDefault(u => u.UserName == "Admin");
      var role = context.Roles.FirstOrDefault(r => r.Name == RolesKey.SuperAdmin.ToString());

      context.UserRoles.Add(new UserRole() { RoleId = role?.Id, UserId = user?.Id });
      context.SaveChanges();
    }
    private static byte[] StringToByteArray(string hex) {
      return Enumerable.Range(0, hex.Length)
          .Where(x => x % 2 == 0)
          .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
          .ToArray();
    }
  }

}