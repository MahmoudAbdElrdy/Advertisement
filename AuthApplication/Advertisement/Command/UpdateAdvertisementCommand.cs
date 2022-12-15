using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Interfaces.Mapper;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Advertisement.Commands
{
    public class EditAdvertisementCommand : IRequest<AdvertisementDto>
    {
        public string Id { get; set; } 
        public string[] Images { get; set; }
   
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsAuction { get; set; }
        public string CityId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
        public int AuctionDays { get; set; }
        public AdCategoryEnum AdCategory { get; set; } 
        public List<string> FreeServiceIds { get; set; }

        class Handler : IRequestHandler<EditAdvertisementCommand, AdvertisementDto>
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

            public async Task<AdvertisementDto> Handle(EditAdvertisementCommand request, CancellationToken cancellationToken)
            {
                Auction auction = new Auction();

                if (request.IsAuction)
                {  
                       //AdsSpaceId = info.AdsSpaces.FirstOrDefault().Id,
                        auction.SeriousSubscriptionAmount = _appSettings.SeriousSubscriptionAmount;
                        auction.AuctionStatus = AuctionStatus.pending;
                        auction.AuctionDays = request.AuctionDays; 
                }
                //var SpaceInfo = await _context.Set<SpaceInfo>().FindAsync(request.Id);
                var ad = await _context.Set<Ad>().Include(x=>x.FreeServices).Include(x=>x.SpaceInfo.Images).FirstOrDefaultAsync(x=>x.Id== request.Id);
                HashSet<FreeService> freeService = new HashSet<FreeService>();
                //if (ad.AdStatuses.FirstOrDefault().Status==AdStatusEnum.Open)
                //{
                //    //ad.FromDate = request.FromDate;
                //    //ad.ToDate = request.ToDate;
                //    //ad.Price = request.Price;
                //    //ad.AdType = request.IsAuction ? AdType.Auction : AdType.Rent;
                //    //ad.Auctions   = new HashSet<Auction> {
                //    //auction
                //   };  
                //}  
                var UserId = auditService.UserId;

                if (request.Images.Any())
                {
                    HashSet<SpaceImage> spaceImages = new HashSet<SpaceImage>();
                    _context.Image.RemoveRange(ad.SpaceInfo.Images);
                   foreach(var image in ad.SpaceInfo.Images)
                    {
                       var IsExti= request.Images.Any(x => x.Contains(image.Imageurl));
                        if(!IsExti)
                         _imageService.DeleteImageIfExist(image.Imageurl);

                    }
                    foreach (var image in request.Images)
                    {

                        var infoImage = new SpaceImage();
                        infoImage.Imageurl = image;
                        // await _imageService.SaveImageAsync(infoImage, image);
                      //  info.Images.Add(infoImage);

                        spaceImages.Add(infoImage);
                    }
                    //ad.SpaceInfo = new SpaceInfo();
                   // ad.SpaceInfo.Images = new HashSet<SpaceImage>();
                    ad.SpaceInfo.Images = spaceImages;
                    _context.Image.AddRange(ad.SpaceInfo.Images);
                }
                _context.FreeServices.RemoveRange(ad.FreeServices);
              
                request.FreeServiceIds.ForEach(x => {
                    _context.FreeServices.Add(new FreeService()
                    {
                        AdId = ad.Id,
                        ServiceTypeId = x
                    });
                });
                ad.AdCategory = request.AdCategory;

                _context.Ads.Update(ad);
             
                await _context.SaveChangesAsync();
                //_context.Set<SpaceInfo>().Where(c=>c.Ads.FirstOrDefault(c=>c.));

                return _mapper.Map<AdvertisementDto>(ad);
            }
        }
    }


}
