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

namespace AuthApplication.UserManagment.Commands {
  public class GetUserCommand : IRequest<UserDto> {
    public string Id { get; set; }     
    }

  public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto> {
  
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(UserManager<User> userManager, IAppDbContext context, IMapper mapper, IImageService imageService) {
      _context = context;
      _mapper = mapper;
    }
    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken) {

            var user = await _context.Users.Include(x => x.Avatar).FirstOrDefaultAsync(u => u.Id == request.Id);
            return _mapper.Map<UserDto>(user);
    }
  }
}
