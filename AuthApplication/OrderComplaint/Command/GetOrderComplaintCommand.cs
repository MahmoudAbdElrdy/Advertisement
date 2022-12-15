using System;
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
    public class GetOrderComplaintCommand : IRequest<OrderComplaintDto>
    {
        public string Id { get; set; }

        class Handler : IRequestHandler<GetOrderComplaintCommand, OrderComplaintDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OrderComplaintDto> Handle(GetOrderComplaintCommand request, CancellationToken cancellationToken)
            {
                var query = await _context.Set<OrderComplaint>()
                  .Include(c => c.AdInterval.Ad.SpaceInfo.Client)
                  .Include(c => c.Client).FirstOrDefaultAsync(x => x.Id == request.Id);

                return _mapper.Map<OrderComplaintDto>(query);
            }
        }
    }
}