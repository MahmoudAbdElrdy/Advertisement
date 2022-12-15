using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AutoMapper;
using Common.Extensions;
using GeoCoordinatePortable;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AuthApplication.Advertisement.Queries
{
    public class GetTopAdsQuery : IRequest<List<AdsDto>>
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        class Handler : IRequestHandler<GetTopAdsQuery, List<AdsDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<AdsDto>> Handle(GetTopAdsQuery request, CancellationToken cancellationToken)
            {
                var userLocation = new GeoCoordinate((double)request.Lat, (double)request.Lng);
              
                var query = _context.Ads
                  .Include(a => a.SpaceInfo)
                  .ThenInclude(s => s.Images)
                  .Include(a => a.SpaceInfo.Client).
                  Include(x=>x.SpaceInfo.Location)
                  .Protected(a => !a.SpaceInfo.Rejected)
                  .OrderByDescending(c => c.CreatedDate).ToList();

                var ads = await query.ToListAsync(cancellationToken);

                var DTO= _mapper.Map<List<AdsDto>>(ads);

                if (request.Lat > 0 && request.Lng > 0)
                {
                    foreach (var item in DTO)
                    {
                        if (double.Parse(item.Lng) > 0 && double.Parse(item.Lng) > 0)
                        {
                            var dist = findDistanceBetweenTwoCoordinates(request.Lng, request.Lat, decimal.Parse(item.Lng), decimal.Parse(item.Lat)) / 1000;
                            item.Distance = dist;
                        }
                    }
                    DTO = DTO.OrderBy(x => x.Distance).Take(10).ToList();
                }

                return DTO;
            }

            decimal findDistanceBetweenTwoCoordinates(decimal langFromRequest, decimal latFromRequest, decimal lanFromAdvertisement, decimal latFromAdvertisement)
            {
                var coord = new GeoCoordinate(Convert.ToDouble(langFromRequest), Convert.ToDouble(latFromRequest));
                var dist = coord.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(lanFromAdvertisement), Convert.ToDouble(latFromAdvertisement)));
                return Convert.ToDecimal(dist);
            }
        }
    }
}