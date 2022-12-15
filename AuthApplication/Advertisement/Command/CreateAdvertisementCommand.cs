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
    public class CreateAdvertisementCommand : IRequest<AdvertisementDto>
    {
        public string[] Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public AdType IsAuction { get; set; }
        public AdCategoryEnum AdCategory { get; set; }
        public string CityId { get; set; }

        //public string ClientId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
        public int AuctionDays { get; set; }
        public List<string> FreeServiceIds { get; set; }


        class Handler : IRequestHandler<CreateAdvertisementCommand, AdvertisementDto>
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

            public async Task<AdvertisementDto> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
            {
                HashSet<FreeService> freeService = new HashSet<FreeService>();

                Auction auction = new Auction();
                if (!string.IsNullOrWhiteSpace(request.FreeServiceIds.FirstOrDefault()))
                {
                    foreach (var serviceid in request.FreeServiceIds)
                    {
                        FreeService f = new FreeService { ServiceTypeId = serviceid };
                        freeService.Add(f);
                    };
                }
                if (request.IsAuction == AdType.Auction)
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
                SpaceInfo info = new SpaceInfo
                {
                    Address = request.Address,
                    CityId = request.CityId,
                    Description = request.Description,
                    Title = request.Title,
                    ClientId = UserId,
                    Location = new SpaceLocation
                    {
                        Lat = request.Lat,
                        Lng = request.Lng
                    },
                    IsAuction = request.IsAuction == AdType.Auction ? true : false,
                    Ads = new HashSet<Ad> {
            new Ad {
              FromDate = request.FromDate,
              ToDate = request.ToDate,
              Price = request.Price,
              AdType = request.IsAuction ,
              Auctions = new HashSet<Auction> {
                auction
              },
              FreeServices=freeService
              ,
              AdStatuses=new HashSet<AdStatus>
              {
                  new AdStatus
                  {
                      Comment="",
                      Status=AdStatusEnum.Open
                  }
              }

            }
          }
                };


                if (request.Images.Any())
                {
                    foreach (var image in request.Images)
                    {
                        var infoImage = new SpaceImage();
                        infoImage.Imageurl = image;
                       // await _imageService.SaveImageAsync(infoImage, image);
                        info.Images.Add(infoImage);
                    }
                }
                _context.Set<SpaceInfo>().Add(info);
                return _mapper.Map<AdvertisementDto>(info);

            }
        }
    }
}