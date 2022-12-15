using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
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

namespace AuthApplication.Advertisement.Command {
  public class GetAllAdvertisementCommand : Paging, IRequest<PageList<AdvertisementDto>> {
    class Handler : IRequestHandler<GetAllAdvertisementCommand, PageList<AdvertisementDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PageList<AdvertisementDto>> Handle(GetAllAdvertisementCommand request, CancellationToken cancellationToken) {
        var query = _context.Set<SpaceInfo>()
          .Include(c => c.Ads)
          .Include(c => c.City)
          .Include(c => c.Client)
          .Include(c => c.City.Country)

          .Include(c => c.Images).AsQueryable().Where(x=> !x.Rejected);
        if (!String.IsNullOrEmpty(request.Filter)) {
          query = query.Where(r => r.Title.Contains(request.Filter));
        }

        var adspageList = await query.ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(request, cancellationToken);

        return adspageList;
      }
    }
  }
}