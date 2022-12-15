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
    public class SetLangCommand : IRequest<UserDto>
    {

        public string Lang { get; set; }
    }

    public class SetLangCommandHandler : IRequestHandler<SetLangCommand, UserDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public SetLangCommandHandler( IAppDbContext context, IMapper mapper, IAuditService auditService)
        {
            _context = context;
            _mapper = mapper;
            _auditService = auditService;
        }
        public async Task<UserDto> Handle(SetLangCommand request, CancellationToken cancellationToken)
        {

            var userId = _auditService.UserId;
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ApiException(ApiExeptionType.NotFound, "notLogin");
            }
            user.UserLang = request.Lang;
            var oldToken = _auditService.WebToken;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            ////_auditService.UserLanguage = request.Lang;
            //_auditService.WebToken = oldToken;

            return _mapper.Map<UserDto>(user);
        }
    }
}
