using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AuthApplication.Advertisement.Queries
{
    public class GetMySpaces : IRequest<List<AdsDto>>
    {
         class Handler : IRequestHandler<GetMySpaces, List<AdsDto>>
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
            public async Task<List<AdsDto>> Handle(GetMySpaces request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId;
                var query = _context.Ads
                  .Include(a => a.AdIntervals)
                  .Include(a => a.SpaceInfo)
                  .ThenInclude(s => s.Images)
                  .Include(a => a.SpaceInfo.Client)
                  //.Protected(a => !a.SpaceInfo.Rejected)
                  .OrderByDescending(c => c.CreatedDate)
                  .Where(c => c.SpaceInfo.ClientId == UserId)
                  .Take(10);
                //_context.Set<Ad>().Where(c => c.AdIntervals.Any(c => c.ClientId == UserId));
                var ads = await query.ToListAsync(cancellationToken);
                return _mapper.Map<List<AdsDto>>(ads);
            }
        }
    }
}