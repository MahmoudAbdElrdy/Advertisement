using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AuthApplication.UserManagment.Commands {
  public class AddUserCommand : IRequest<UserDto> {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string[] Roles { get; set; }
    public string Avatar { get; set; }

    class Handler : IRequestHandler<AddUserCommand, UserDto> {

      private readonly IMapper _mapper;
      private readonly IImageService _imageService;
      private readonly UserManager<User> _userManager;

      public Handler(UserManager<User> userManager, IMapper mapper, IImageService imageService) {
        _userManager = userManager;
        _mapper = mapper;
        _imageService = imageService;
      }
      public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken) {

         var user = new User() {
          UserName = request.UserName.ToLower().Trim(),
          Email = request.Email,
          PhoneNumber = request.PhoneNumber,
          NormalizedEmail = request.Email.ToUpper(),
          NormalizedUserName = request.UserName.ToUpper(),
          FirstName = request.FirstName,
          LastName = request.LastName,
        };

        if (!string.IsNullOrEmpty(request.Avatar) ) {
          user.Avatar = user.Avatar == null ? new UserAvatar() : user.Avatar;
          await _imageService.SaveImageAsync(user.Avatar, request.Avatar);
        }
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
          throw new ApiException(ApiExeptionType.ValidationError, result.Errors.Select(e => new ErrorResult(e.Code, e.Description)).ToArray());

        if (request.Roles.Length > 0)
          await _userManager.AddToRolesAsync(user, request.Roles);


        return _mapper.Map<UserDto>(user);


      }
    }
  }


}
