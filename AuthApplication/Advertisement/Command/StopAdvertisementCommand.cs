using AuthApplication.Advertisement.Dto;
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

namespace AuthApplication.Advertisement.Commands
{
    public class StopAdvertisementCommand : IRequest<AdvertisementDto>
    {
        public string Id { get; set; }


        class Handler : IRequestHandler<StopAdvertisementCommand, AdvertisementDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly IImageService _imageService;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
                this._imageService = imageService;
            }

            public async Task<AdvertisementDto> Handle(StopAdvertisementCommand request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId; 
                var adStatus = _context.Set<AdStatus>().Where(c => c.AdId == request.Id && c.Ad.SpaceInfo.ClientId == UserId).FirstOrDefault();
                if (adStatus != null)
                {
                    adStatus.Status = AdStatusEnum.Closed;
                    _context.Set<AdStatus>().Update(adStatus);
                    var ad = await _context.Set<Ad>().FindAsync(request.Id); 
                    return _mapper.Map<AdvertisementDto>(ad);
                }
                else
                {
                  return  new AdvertisementDto();
                }
            }
        }
    }


}
