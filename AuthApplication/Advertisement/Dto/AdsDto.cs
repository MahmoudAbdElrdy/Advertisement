using System;
using System.Collections.Generic;
using System.Linq;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Advertisement.Dto
{
    public class AdsDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string SpaceId { get; set; }
        public AdType AdType { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string Title { get; set; }
        public string CityId { get; set; }
        public string CountryId { get; set; }
        public string IntervalId { get; set; }

        public string vendorName { get; set; }
        public decimal Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> Images { get; set; }
        public bool Rejected { get; set; }
        public bool? IsFavorite { get; set; }
        public decimal? Distance { get; set; }
        public AdCategoryEnum AdCategory { get; set; }

        public HashSet<FreeServiceDto> FreeServices { get; set; } = new HashSet<FreeServiceDto>();

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Ad, AdsDto>()
              .ForMember(d => d.Lat, cfg => cfg.MapFrom(c => c.SpaceInfo.Location.Lat))
              .ForMember(d => d.Lng, cfg => cfg.MapFrom(c => c.SpaceInfo.Location.Lng))
              .ForMember(d => d.SpaceId, cfg => cfg.MapFrom(c => c.SpaceInfo.Id))
              .ForMember(d => d.FromDate, cfg => cfg.MapFrom(c => c.AdIntervals.FirstOrDefault().FromDate))
              .ForMember(d => d.ToDate, cfg => cfg.MapFrom(c => c.AdIntervals.FirstOrDefault().ToDate))
              .ForMember(d => d.Description, cfg => cfg.MapFrom(c => c.SpaceInfo.Description))
              .ForMember(d => d.AdCategory, cfg => cfg.MapFrom(c => c.AdCategory))
              .ForMember(d => d.Address, cfg => cfg.MapFrom(c => c.SpaceInfo.Address))
              .ForMember(d => d.Rejected, cfg => cfg.MapFrom(c => c.SpaceInfo.Rejected))
              .ForMember(d => d.CityId, cfg => cfg.MapFrom(c => c.SpaceInfo.CityId))
              .ForMember(d => d.CountryId, cfg => cfg.MapFrom(c => c.SpaceInfo.City.Country.Id))
              .ForMember(d => d.Title, cfg => cfg.MapFrom(c => c.SpaceInfo.Title))
              .ForMember(d => d.FromDate, cfg => cfg.MapFrom(c => c.AdIntervals.FirstOrDefault().FromDate))
              .ForMember(d => d.IntervalId, cfg => cfg.MapFrom(c => c.AdIntervals.FirstOrDefault().Id))
              .ForMember(d => d.ToDate, cfg => cfg.MapFrom(c => c.AdIntervals.FirstOrDefault().ToDate))
              .ForMember(d => d.vendorName, cfg => cfg.MapFrom(c => c.SpaceInfo.Client.FullName))
              .ForMember(d => d.FreeServices, cfg => cfg.MapFrom(c => c.FreeServices.Select(c => new FreeServiceDto { AdId = c.AdId, ServiceTypeId = c.ServiceTypeId })))
              .ForMember(d => d.Images, cfg => cfg.MapFrom(c => c.SpaceInfo.Images != null ? c.SpaceInfo.Images.Select(a => a.Imageurl).ToList() : new List<string>()));              
        }

        public class FreeServiceDto  
        {
            public string AdId { get; set; }
            public string ServiceTypeId { get; set; }
            
        }

    }
}
