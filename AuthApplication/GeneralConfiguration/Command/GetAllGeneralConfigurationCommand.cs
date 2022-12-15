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
  public class GetAllGeneralConfigurationCommand : Paging, IRequest<PageList<GeneralConfigurationDto>> {
    class Handler : IRequestHandler<GetAllGeneralConfigurationCommand, PageList<GeneralConfigurationDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PageList<GeneralConfigurationDto>> Handle(GetAllGeneralConfigurationCommand request, CancellationToken cancellationToken) {
                
                var query = _context.Set<GeneralConfiguration>();

        
        var adspageList = await query.ProjectTo<GeneralConfigurationDto>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(request, cancellationToken);

        return adspageList;
      }
    }
  }
}