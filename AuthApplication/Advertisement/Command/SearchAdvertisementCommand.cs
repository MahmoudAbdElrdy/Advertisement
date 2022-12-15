using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Advertisement.Command
{
    public class SearchAdvertisementCommand : Paging, IRequest<PageList<AdvertisementDto>>
    {
        public List<string> CountryId { get; set; }
        public List<string> CityId { get; set; }
        public string Title { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public List<AdCategoryEnum> AdCategoryies { get; set; }
 
        class Handler : IRequestHandler<SearchAdvertisementCommand, PageList<AdvertisementDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PageList<AdvertisementDto>> Handle(SearchAdvertisementCommand request, CancellationToken cancellationToken)
            {
                var query = _context.Set<Ad>()
                  .Include(c => c.SpaceInfo)
                  .ThenInclude(c => c.City)
                  .Include(c => c.SpaceInfo)
                  .ThenInclude(c => c.Client)
                  .Include(c => c.SpaceInfo) 
                  .ThenInclude(c => c.Images).AsQueryable();
                if (!String.IsNullOrEmpty(request.Filter))
                {
                    query = query.Where(r => r.SpaceInfo.Title.Contains(request.Filter));
                }
                if (!String.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(r => r.SpaceInfo.Title.Contains(request.Title));
                }
                if (request.CityId.Count>0)
                {
                    query = query.Where(r => request.CityId.Contains(r.SpaceInfo.CityId));
                   
                } 
                if ((request.CountryId.Count>0))
                {
                    //  query = query.Where(r => r.SpaceInfo.City.Country.Id==request.CountryId);
                    query = query.Where(r => request.CountryId.Contains(r.SpaceInfo.City.CountryId));
                }

                if (request.MaxPrice > 0 )
                {
                    query = query.Where(r => r.Price <= request.MaxPrice);
                }
                if (request.MinPrice > 0 )
                {
                    query = query.Where(r => r.Price >= request.MinPrice);
                }
                if (request.AdCategoryies.Count > 0 )
                {
                    query = query.Where(r =>request.AdCategoryies.Contains(r.AdCategory));
                }

                query = query.Where(r => r.SpaceInfo.Rejected==false);
                  
                return await query.ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider)
                                          .ToPagedListAsync(request, cancellationToken);
            }
        }
    }
}