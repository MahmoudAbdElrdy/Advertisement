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
    public class GetAdvertisementByIdCommand :   IRequest<AdsDto>
    {
        public string Id { get; set; }

        class Handler : IRequestHandler<GetAdvertisementByIdCommand, AdsDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AdsDto> Handle(GetAdvertisementByIdCommand request, CancellationToken cancellationToken)
            {
                var Ad = _context.Set<Ad>().Include(c=>c.SpaceInfo).ThenInclude(c=>c.Images).Include(c=>c.SpaceInfo).ThenInclude(c=>c.City).ThenInclude(c=>c.Country).Include(c=>c.FreeServices).Include(c=>c.AdIntervals).FirstOrDefault(c => c.Id == request.Id);        
                return _mapper.Map<AdsDto>(Ad);
            }
        }
    }


}
