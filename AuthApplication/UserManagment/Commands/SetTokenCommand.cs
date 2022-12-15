using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Common.Interfaces;
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
    public class SetTokenCommand : IRequest<UserDto>
    {

        public string WebToken { get; set; }
    }

    public class SetTokenCommandHandler : IRequestHandler<SetTokenCommand, UserDto>
    {
         private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;


        public SetTokenCommandHandler(IAppDbContext context, IMapper mapper, IAuditService auditService)
        {
            _context = context;
            _mapper = mapper;
            _auditService = auditService;

        }
        public async Task<UserDto> Handle(SetTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = _auditService.UserId;
            var user = await _context.Users.FindAsync(userId);

            if (user==null)
            {
                throw new ApiException(ApiExeptionType.NotFound, "notLogin");
            }

            user.WebToken = request.WebToken;
            var oldLang = _auditService.UserLanguage;
                _context.Users.Update(user);
            await _context.SaveChangesAsync();
            //_auditService.WebToken= request.WebToken;
            //_auditService.UserLanguage = oldLang;

            return _mapper.Map<UserDto>(user);
        }
    }
}
