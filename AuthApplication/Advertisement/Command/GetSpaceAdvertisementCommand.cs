using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Interfaces.Mapper;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Advertisement.Commands
{
    public class GetSpaceAdvertisementCommand : IRequest<SpaceInfoDto>
    {
        public string Id { get; set; }

        class Handler : IRequestHandler<GetSpaceAdvertisementCommand, SpaceInfoDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SpaceInfoDto> Handle(GetSpaceAdvertisementCommand request, CancellationToken cancellationToken)
            {
                var spaceInfoOId = _context.Set<Ad>().Include(x => x.Ratings).FirstOrDefault(c => c.Id == request.Id);
                var spaceInfo = _context.Set<SpaceInfo>()
                    .Include(c => c.Location)
                    .Include(c => c.Ads)
                    .ThenInclude(c => c.AdIntervals)
                    .Include(c => c.City)
                    .Include(c => c.Client)
                    .Include(c => c.City.Country)
                    .Include(c => c.Images)
                    .FirstOrDefault(c => c.Id == spaceInfoOId.SpaceInfoId);

                var Dto = _mapper.Map<SpaceInfoDto>(spaceInfo);

                if (spaceInfoOId.Ratings.Count > 0)
                {
                    Dto.RatingValue = spaceInfoOId.Ratings.Sum(c => c.RatingValue) / (spaceInfoOId.Ratings.Count() == 0 ? 1 : spaceInfoOId.Ratings.Count());

                    Dto.RatingValue = (double?)Math.Round((decimal)Dto.RatingValue, 2);
                }
                return Dto;
            }
        }
    }


}
