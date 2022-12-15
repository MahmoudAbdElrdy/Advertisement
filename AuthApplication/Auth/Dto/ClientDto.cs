using AuthDomain.Entities.Ads;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Auth.Dto {
  public class ClientDto:IHaveCustomMapping {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Error { get; set; } 
    public void CreateMappings(Profile configuration) {
      configuration.CreateMap<Client, ClientDto>().ForMember(s => s.Error, t => t.Ignore());
            
    }
  }
}