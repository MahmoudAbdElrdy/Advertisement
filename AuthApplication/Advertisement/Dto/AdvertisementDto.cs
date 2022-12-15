using System;
using System.Linq;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;

namespace AuthApplication.Advertisement.Dto
{
    public class AdvertisementDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string AdId { get; set; } 
        public string VendorName { get; set; }
         public string Image { get; set; }
        public string Title { get; set; }
        public AdType AdType { get; set; }
         public string Description { get; set; }
        public string Address { get; set; }
        public bool IsAuction { get; set; }
        public bool Rejected { get; set; }
        public string CityId { get; set; }
        public LocalizedData CityName { get; set; }
        public string ClientId { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
      //  public AuctionDto AuctionDto { get; set; } = new AuctionDto();
        public Decimal SeriousSubscriptionAmount { get; set; }
        public AuctionStatus AuctionStatus { get; set; }
        public int AuctionDays { get; set; }
        public AdCategoryEnum AdCategory { get; set; }
        public bool? IsFavorite { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SpaceInfo, AdvertisementDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.AdId, cfg => cfg.MapFrom(c => c.Ads.FirstOrDefault().Id))
              .ForMember(d => d.Address, cfg => cfg.MapFrom(c => c.Address))
              .ForMember(d => d.Lat, cfg => cfg.MapFrom(c => c.Location.Lat))
              .ForMember(d => d.Lng, cfg => cfg.MapFrom(c => c.Location.Lng))
              .ForMember(d => d.Price, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Price))
              .ForMember(d => d.SeriousSubscriptionAmount, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Auctions.FirstOrDefault().SeriousSubscriptionAmount))
              .ForMember(d => d.AuctionDays, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Auctions.FirstOrDefault().AuctionDays))
              .ForMember(d => d.AuctionStatus, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Auctions.FirstOrDefault().AuctionStatus))
            //.ForMember(d => d.AuctionDto, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Auctions.Select(c=>new AuctionDto {  AuctionDays=c.AuctionDays,AuctionStatus=c.AuctionStatus,SeriousSubscriptionAmount=c.SeriousSubscriptionAmount })))
              .ForMember(d => d.IsAuction, cfg => cfg.MapFrom(c => c.IsAuction))
              .ForMember(d => d.CityName, cfg => cfg.MapFrom(c => c.City.Name))
              .ForMember(d => d.AdType, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdType)) 
              .ForMember(d => d.AdCategory, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdCategory)) 
              .ForMember(d => d.VendorName, cfg => cfg.MapFrom(c => c.Client.FullName))
              .ForMember(d => d.Description, cfg => cfg.MapFrom(c => c.Description)) 
              .ForMember(d => d.FromDate, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().FromDate))
              .ForMember(d => d.ToDate, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().ToDate))
              .ForMember(d => d.Image, cfg => cfg.MapFrom(c => c.Images != null ? c.Images.FirstOrDefault().Imageurl : ""))

              ;
            configuration.CreateMap<Ad, AdvertisementDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.AdId, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.Address, cfg => cfg.MapFrom(c => c.SpaceInfo.Address))
              .ForMember(d => d.Lat, cfg => cfg.MapFrom(c => c.SpaceInfo.Location.Lat))
              .ForMember(d => d.Lng, cfg => cfg.MapFrom(c => c.SpaceInfo.Location.Lng))
              .ForMember(d => d.Price, cfg => cfg.MapFrom(c => c.Price))
              .ForMember(d => d.SeriousSubscriptionAmount, cfg => cfg.MapFrom(c => c.Auctions.FirstOrDefault().SeriousSubscriptionAmount))
              .ForMember(d => d.AuctionDays, cfg => cfg.MapFrom(c => c.Auctions.FirstOrDefault().AuctionDays))
              .ForMember(d => d.AuctionStatus, cfg => cfg.MapFrom(c => c.Auctions.FirstOrDefault().AuctionStatus))
            //.ForMember(d => d.AuctionDto, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Auctions.Select(c=>new AuctionDto {  AuctionDays=c.AuctionDays,AuctionStatus=c.AuctionStatus,SeriousSubscriptionAmount=c.SeriousSubscriptionAmount })))
              .ForMember(d => d.IsAuction, cfg => cfg.MapFrom(c => c.SpaceInfo.IsAuction))
              .ForMember(d => d.CityName, cfg => cfg.MapFrom(c => c.SpaceInfo.City.Name))
              .ForMember(d => d.AdType, cfg => cfg.MapFrom(c => c.SpaceInfo.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdType)) 
              .ForMember(d => d.AdCategory, cfg => cfg.MapFrom(c => c.SpaceInfo.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdCategory)) 
              .ForMember(d => d.VendorName, cfg => cfg.MapFrom(c => c.SpaceInfo.Client.FullName))
              .ForMember(d => d.Description, cfg => cfg.MapFrom(c => c.SpaceInfo.Description)) 
              .ForMember(d => d.FromDate, cfg => cfg.MapFrom(c => c.SpaceInfo.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().FromDate))
              .ForMember(d => d.ToDate, cfg => cfg.MapFrom(c => c.SpaceInfo.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().ToDate))
              .ForMember(d => d.Image, cfg => cfg.MapFrom(c => c.SpaceInfo.Images != null ? c.SpaceInfo.Images.FirstOrDefault().Imageurl : ""))

              ;
        }
    }
    

    }
