using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AuthApplication.Advertisement.Queries
{
    public class GetMyServices : IRequest<List<ServicesDto>>
    {
         class Handler : IRequestHandler<GetMyServices, List<ServicesDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;

            public Handler(IAppDbContext context, IAuditService auditService, IMapper mapper)
            {
                this.auditService = auditService;
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ServicesDto>> Handle(GetMyServices request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId;
                var query = _context.Services
                  .Include(a => a.User)
                  .Include(a => a.PaidServices)
                  .ThenInclude(s => s.AdInterval.Client)
                  .Include(a => a.ServiceType)
                  .OrderByDescending(c => c.Id)
                  .Where(c => c.PaidServices.Any(c => c.AdInterval.ClientId == UserId));
                var Services = await query.ToListAsync(cancellationToken);
                return _mapper.Map<List<ServicesDto>>(Services);
            }
        }
    }
}