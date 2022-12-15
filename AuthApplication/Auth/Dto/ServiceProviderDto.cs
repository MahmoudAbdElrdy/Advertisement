using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Auth.Dto {
  public class ServiceProviderDto : IHaveCustomMapping {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public void CreateMappings(Profile configuration) {
      configuration.CreateMap<User, ServiceProviderDto>()
        ;
    }
  }
}