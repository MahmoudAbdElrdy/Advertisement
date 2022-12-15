using System;
using System.Collections.Generic;
using System.Linq;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;

namespace AuthApplication.Advertisement.Dto
{
    public class SpaceInfoDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public List<string> Images { get; set; }
        public string Title { get; set; }
        public AdType AdType { get; set; }
        public AdCategoryEnum AdCategory { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsAuction { get; set; }
        public bool Rejected { get; set; }
        public string CityId { get; set; }
        public decimal Price { get; set; }
        public LocalizedData CityName { get; set; }
         public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public  string  AdId   { get; set; }
        //public AdIntervalDto AdInterval  { get; set; }
        public DateTime AdIntervalFromDate { get; set; }
        public DateTime AdIntervalToDate { get; set; }
         public double? RatingValue { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SpaceInfo, SpaceInfoDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              //.ForMember(d => d.Ad, cfg => cfg.MapFrom(c =>c.Ads.LastOrDefault(c=>new AdDto { Id=c.Id,Price=c.Price, IsDeleted =c.IsDeleted,AdType=c.AdType,FromDate=c.FromDate,ToDate=c.ToDate}) ))
              .ForMember(d => d.AdId, cfg => cfg.MapFrom(c =>c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().Id))
              .ForMember(d => d.Price, cfg => cfg.MapFrom(c =>c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().Price))
              .ForMember(d => d.AdIntervalFromDate, cfg => cfg.MapFrom(c =>c.Ads.LastOrDefault().AdIntervals.LastOrDefault().FromDate ))
             .ForMember(d => d.AdIntervalToDate, cfg => cfg.MapFrom(c =>c.Ads.LastOrDefault().AdIntervals.LastOrDefault().ToDate ))
              .ForMember(d => d.Address, cfg => cfg.MapFrom(c => c.Address))
              .ForMember(d => d.Lat, cfg => cfg.MapFrom(c => c.Location.Lat))
              .ForMember(d => d.Lng, cfg => cfg.MapFrom(c => c.Location.Lng))
            //.ForMember(d => d.Price, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c=>c.CreatedDate).FirstOrDefault().Price))
             .ForMember(d => d.CityName, cfg => cfg.MapFrom(c => c.City.Name))
             .ForMember(d => d.AdType, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdType)) 
             .ForMember(d => d.AdCategory, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().AdCategory)) 
             .ForMember(d => d.VendorName, cfg => cfg.MapFrom(c => c.Client.FullName))
             .ForMember(d => d.VendorPhone, cfg => cfg.MapFrom(c => c.Client.PhoneNumber))
             .ForMember(d => d.VendorEmail, cfg => cfg.MapFrom(c => c.Client.Email))
              .ForMember(d => d.Description, cfg => cfg.MapFrom(c => c.Description))
            //   .ForMember(d => d.FromDate, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().FromDate))
              // .ForMember(d => d.ToDate, cfg => cfg.MapFrom(c => c.Ads.OrderBy(c => c.CreatedDate).FirstOrDefault().ToDate))
               .ForMember(d => d.Images, cfg => cfg.MapFrom(c => c.Images != null ? c.Images.Select(a=>a.Imageurl).ToList() : new List<string>()))

              ;
        }

        public class AdDto
        {
            public string Id { get; set; }
            public decimal Price { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public AdType AdType { get; set; }
            public AdCategoryEnum AdCategory { get; set; }
        }
        //public class AdIntervalDto
        //{
        //    public DateTime? FromDate { get; set; }
        //    public DateTime ?ToDate { get; set; }

        //}

    }

}
