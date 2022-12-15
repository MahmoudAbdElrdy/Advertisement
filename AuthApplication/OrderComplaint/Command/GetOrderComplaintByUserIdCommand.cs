using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Complaint.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Complaint.Command
{
    public class GetOrderComplaintByUserIdCommand : IRequest<List<OrderComplaintDto>>
    {
        class Handler : IRequestHandler<GetOrderComplaintByUserIdCommand, List<OrderComplaintDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;


            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService)
            {
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;

            }

            public async Task<List<OrderComplaintDto>> Handle(GetOrderComplaintByUserIdCommand request, CancellationToken cancellationToken)
            {
                var userId = auditService.UserId;
                var query = await _context.Set<OrderComplaint>()
                  .Include(c => c.AdInterval.Ad.SpaceInfo)
                  .Include(c => c.Client).Where(x => x.AdInterval.Ad.SpaceInfo.ClientId == userId).ToListAsync();

                return _mapper.Map<List<OrderComplaintDto>>(query);
            }
        }
    }
}