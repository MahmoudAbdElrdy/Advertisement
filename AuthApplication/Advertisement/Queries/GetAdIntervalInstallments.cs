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
    public class GetAdIntervalInstallments : IRequest<List<AdIntervalInstallmentDto>>
    {
        public string AdIntervalId { get; set; }
        class Handler : IRequestHandler<GetAdIntervalInstallments, List<AdIntervalInstallmentDto>>
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
            public async Task<List<AdIntervalInstallmentDto>> Handle(GetAdIntervalInstallments request, CancellationToken cancellationToken)
            {
                 var query = _context.Installments
                   .Where(c => c.AdIntervalId == request.AdIntervalId);

                 var ads = await query.ToListAsync(cancellationToken);
                return _mapper.Map<List<AdIntervalInstallmentDto>>(ads);
            }
        }
    }
}