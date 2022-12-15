using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.UserManagment.Commands
{
    public class EditUserCommand : IRequest<UserDto>
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        public string Avatar { get; set; }

    }

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IImageService _imageService;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(UserManager<User> userManager, IAppDbContext context, IMapper mapper, IImageService imageService)
        {
            _imageService = imageService;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Include(u => u.Avatar).FirstOrDefaultAsync(u => u.Id == request.Id);

            user.Email = request.Email;
            user.NormalizedEmail = request.Email.ToUpper();
            user.PhoneNumber = request.PhoneNumber;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.Avatar)&&user.Avatar?.Imageurl!=request.Avatar)
            {
                user.Avatar = user.Avatar == null ? new UserAvatar() : user.Avatar;
                user.Avatar.UserId = user.Id;
                user.Avatar.Imageurl = request.Avatar;
                // await _imageService.SaveImageAsync(user.Avatar, request.Avatar);
                 _context.Set<UserAvatar>().Update(user.Avatar);
                //_context.Users.Update(user);
            }


            if (!String.IsNullOrEmpty(request.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, request.Password);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
                throw new ApiException(ApiExeptionType.NotFound, result.Errors.Select(s => new ErrorResult(s.Code, s.Description)).ToArray());


            if (request.Roles != null && request.Roles.Length > 0)
                await _userManager.AddToRolesAsync(user, request.Roles);



            _context.Users.Update(user);
            return _mapper.Map<UserDto>(user);
        }
    }
}
