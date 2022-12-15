using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using AutoMapper;
using Common;
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

namespace AuthApplication.Advertisement.Commands
{
    public class DisableAdvertisementCommand : IRequest<SpaceInfo>
    {
        public string Id { get; set; }
        public bool Enable { get; set; }
        
        class Handler : IRequestHandler<DisableAdvertisementCommand, SpaceInfo>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SpaceInfo> Handle(DisableAdvertisementCommand request, CancellationToken cancellationToken)
            {
                
                var spaceInfo = await _context.Set<SpaceInfo>().FindAsync(request.Id);
                if (spaceInfo == null)
                {
                    throw new ApiException(ApiExeptionType.NotFound);
                }
                spaceInfo.Rejected = request.Enable;
                _context.Set<SpaceInfo>().Update(spaceInfo);
               await _context.SaveChangesAsync();
                return _mapper.Map<SpaceInfo>(spaceInfo);
                 
            }
        }
    }


}
