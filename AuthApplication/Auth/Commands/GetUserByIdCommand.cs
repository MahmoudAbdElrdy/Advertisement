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
  public class GetUserByIdCommand : IRequest<UserDto> {
    public string UserId { get; set; }
  
    class Handler:IRequestHandler<GetUserByIdCommand, UserDto> {

      private readonly UserManager<User> _userManager;
      private readonly IMapper _mapper;

      public Handler(UserManager<User> userManager,IMapper mapper) {
        _userManager = userManager;
        _mapper = mapper;
      }
      public async Task<UserDto> Handle(GetUserByIdCommand request, CancellationToken cancellationToken) {
     
                // await _userManager.CreateAsync(user, request.Password);
                var result = await _userManager.FindByIdAsync(request.UserId);

 
        return _mapper.Map<UserDto>(result);

      }
    }
    
  }
}