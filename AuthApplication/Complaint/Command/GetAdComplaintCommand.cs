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
    public class GetAdComplaintCommand : IRequest<AdComplaintDto>
    {
        public string Id { get; set; }

        class Handler : IRequestHandler<GetAdComplaintCommand, AdComplaintDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AdComplaintDto> Handle(GetAdComplaintCommand request, CancellationToken cancellationToken)
            {
                var query = await _context.Set<AdComplaint>()
                  .Include(c => c.Ad)
                  .Include(c => c.Client).FirstOrDefaultAsync(x => x.Id == request.Id);

                return _mapper.Map<AdComplaintDto>(query);
            }
        }
    }
}