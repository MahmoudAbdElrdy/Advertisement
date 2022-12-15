using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Auth.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthApplication.Auth.Commands {
  public class ServiceProviderRegisterCommand : IRequest<ServiceProviderDto> {
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    class Handler:IRequestHandler<ServiceProviderRegisterCommand, ServiceProviderDto> {

      private readonly UserManager<User> _userManager;
      private readonly IMapper _mapper;

      public Handler(UserManager<User> userManager,IMapper mapper) {
        _userManager = userManager;
        _mapper = mapper;
      }
      public async Task<ServiceProviderDto> Handle(ServiceProviderRegisterCommand request, CancellationToken cancellationToken) {
        var user = new User {
          UserName = request.Email,
          Email = request.Email,
          PhoneNumber = request.PhoneNumber,
          FirstName = request.FullName
        };


      //  await _userManager.CreateAsync(user, request.Password);
          var result = await _userManager.CreateAsync(user, request.Password);

          var ee= await _userManager.AddToRoleAsync(user, RolesKey.ServiceProvider.ToString());

        return _mapper.Map<ServiceProviderDto>(user);

      }
    }
    
  }
}