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
  public class GetAllAdComplaintCommand : Paging, IRequest<PageList<AdComplaintDto>> {
    class Handler : IRequestHandler<GetAllAdComplaintCommand, PageList<AdComplaintDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PageList<AdComplaintDto>> Handle(GetAllAdComplaintCommand request, CancellationToken cancellationToken) {
                var query = _context.Set<AdComplaint>()
                  .Include(c => c.Ad)
                  .Include(c => c.Client);

        
        var adspageList = await query.ProjectTo<AdComplaintDto>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(request, cancellationToken);

        return adspageList;
      }
    }
  }
}