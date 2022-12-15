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

namespace AuthApplication.Complaint.Command {
  public class GetAllOrderComplaintCommand : Paging, IRequest<PageList<OrderComplaintDto>> {
    class Handler : IRequestHandler<GetAllOrderComplaintCommand, PageList<OrderComplaintDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PageList<OrderComplaintDto>> Handle(GetAllOrderComplaintCommand request, CancellationToken cancellationToken) {
                var query = _context.Set<OrderComplaint>()
                  .Include(c => c.AdInterval.Client)
                  .Include(c => c.Client);

        
        var adspageList = await query.ProjectTo<OrderComplaintDto>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(request, cancellationToken);

        return adspageList;
      }
    }
  }
}