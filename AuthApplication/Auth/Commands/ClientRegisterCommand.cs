using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Auth.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthApplication.Auth.Commands
{
    public class ClientRegisterCommand : IRequest<ClientDto>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string Lang { get; set; }
        public string WebToken { get; set; }
        public List<string> Roles { get; set; }
        class Handler : IRequestHandler<ClientRegisterCommand, ClientDto>
        {

            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IImageService _imageService;
            private readonly IAppDbContext _context;



            public Handler(UserManager<User> userManager, IMapper mapper, IImageService imageService, IAppDbContext context)
            {
                _userManager = userManager;
                _mapper = mapper;
                _imageService = imageService;
                _context = context;

            }
            public async Task<ClientDto> Handle(ClientRegisterCommand request, CancellationToken cancellationToken)
            {
                var user = new Client
                {
                    UserName = request.PhoneNumber,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    WebToken=request.WebToken,
                    UserLang= request.Lang
                };
                var Name = await _userManager.FindByNameAsync(request.PhoneNumber);
                if (Name != null)
                {
                    ClientDto client = new ClientDto();
                    client.Error = "رقم الجوال تم تسجيله من فبل";
                    return client;
                }
                var Email =  _context.Users.FirstOrDefault(x=>x.Email==request.Email);
                if (Email != null)
                {
                    ClientDto client = new ClientDto();
                    client.Error = "البريد الالكتروني تم تسجيله من قبل";
                    return client;
                }
                var result = await _userManager.CreateAsync(user, request.Password);
            
                if (!result.Succeeded)
                {
                    ClientDto client = new ClientDto();
                    client.Error = result.Errors.Select(e=>e.Code).ToArray()[0];
                    return client;
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.Avatar))
                    {
                        user.Avatar = user.Avatar == null ? new UserAvatar() : user.Avatar;
                        user.Avatar.UserId = user.Id;
                        user.Avatar.Imageurl = request.Avatar;
                       // await _imageService.SaveImageAsync(user.Avatar, request.Avatar);
                        await _context.Set<UserAvatar>().AddAsync(user.Avatar);
                        _context.Users.Add(user);
                    }
                    await _userManager.AddToRolesAsync(user, request.Roles);

                    return _mapper.Map<ClientDto>(user);

                }
                   // throw new ApiException(ApiExeptionType.ValidationError, result.Errors.Select(e => new ErrorResult(e.Code, e.Description)).ToArray());
               
            }
        }

    }
}