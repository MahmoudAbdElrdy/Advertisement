using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Auth.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using AutoMapper;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Auth.Commands
{
    public class IsUserHasCommercialRecordCommand : IRequest<bool>
    {

        class Handler : IRequestHandler<IsUserHasCommercialRecordCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly IAuditService auditService;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IAuditService auditService)
            {
                this.auditService = auditService;
                _context = context;
            }
            public async Task<bool> Handle(IsUserHasCommercialRecordCommand request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId;
                return _context.Set<Client>().Include(c => c.Documents).Where(c => c.Id == UserId).Any(c => c.Documents.Any(d => !d.Rejected)); 
            }
        }

    }
}