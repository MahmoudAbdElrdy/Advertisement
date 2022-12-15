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
    public class AdIntervalsDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string AdTitle { get; set; } 
        public string ClientName { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
  
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AdInterval, AdIntervalsDto>()
              .ForMember(d => d.ClientName, cfg => cfg.MapFrom(c => c.Client.FullName))
              .ForMember(d => d.AdTitle, cfg => cfg.MapFrom(c => c.Ad.SpaceInfo.Title)) 
              ;
        }  
    }
}
