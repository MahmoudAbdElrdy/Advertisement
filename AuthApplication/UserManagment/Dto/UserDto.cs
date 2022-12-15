using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using System.Linq;

namespace AuthApplication.UserManagment.Dto
{
    public class UserDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserDto>()
              .ForMember(a => a.Avatar, cfg => cfg.MapFrom(a => a.Avatar != null ? a.Avatar.Imageurl : ""))
              .ForMember(a => a.Roles, cfg => cfg.MapFrom(a => a.UserRoles.Count > 0 ? a.UserRoles.Select(s => s.Role.Name).ToArray() : default));
        }
    }
}