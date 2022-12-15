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
    public class GetAdIntervals : IRequest<List<AdIntervalsDto>>
    {
        public string AdId { get; set; }
        class Handler : IRequestHandler<GetAdIntervals, List<AdIntervalsDto>>
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
            public async Task<List<AdIntervalsDto>> Handle(GetAdIntervals request, CancellationToken cancellationToken)
            {
                 var query = _context.AdIntervals
                  .Include(a => a.Ad)
                  .Include(a => a.Client)
                  .Include(a => a.Ad.SpaceInfo)
                  .OrderByDescending(c => c.CreatedDate)
                  .Where(c => c.AdId == request.AdId)
                   ;

                 var ads = await query.ToListAsync(cancellationToken);
                return _mapper.Map<List<AdIntervalsDto>>(ads);
            }
        }
    }
}