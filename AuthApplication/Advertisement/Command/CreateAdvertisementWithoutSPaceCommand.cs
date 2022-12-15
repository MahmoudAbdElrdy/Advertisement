using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command
{
    public class CreateAdvertisementWithoutSPaceCommand : IRequest<AdvertisementDto>
    {
        public string SpaceInfoId { get; set; } 
        public string[] Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsAuction { get; set; }

        //public string ClientId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
        public int AuctionDays { get; set; }

        public List<string> FreeServiceIds { get; set; }


        class Handler : IRequestHandler<CreateAdvertisementWithoutSPaceCommand, AdvertisementDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly IImageService _imageService;
            AppSettings _appSettings;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                _appSettings = systemSetting.Value;
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
                this._imageService = imageService;
            }

            public async Task<AdvertisementDto> Handle(CreateAdvertisementWithoutSPaceCommand request, CancellationToken cancellationToken)
            {
                HashSet<FreeService> freeService = new HashSet<FreeService>();

                Auction auction = new Auction();
                if (request.FreeServiceIds.Count > 0)
                {
                    foreach (var serviceid in request.FreeServiceIds)
                    {
                        FreeService f = new FreeService { ServiceTypeId = serviceid };
                        freeService.Add(f);
                    };
                }
                if (request.IsAuction)
                {
                    auction = new Auction
                    {
                        //AdsSpaceId = info.AdsSpaces.FirstOrDefault().Id,
                        SeriousSubscriptionAmount = _appSettings.SeriousSubscriptionAmount,
                        AuctionStatus = AuctionStatus.pending,
                        AuctionDays = request.AuctionDays,
                    };
                }

                var UserId = auditService.UserId;

         Ad ad =
        new Ad
        {
            SpaceInfoId= request.SpaceInfoId,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Price = request.Price,
            AdType = request.IsAuction ? AdType.Auction : AdType.Rent,
            Auctions = new HashSet<Auction> {
                auction
          },
            FreeServices = freeService
          ,
            AdStatuses = new HashSet<AdStatus>
          {
                  new AdStatus
                  {
                      Comment="",
                      Status=AdStatusEnum.Open
                  }
          } 
        };


                _context.Set<Ad>().Add(ad);
                return _mapper.Map<AdvertisementDto>(ad);

            }
        }
    }
}