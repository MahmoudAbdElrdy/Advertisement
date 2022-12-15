using System;
using System.Collections.Generic;
using System.Linq;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;

namespace AuthApplication.Advertisement.Dto
{
    public class PaidServicesOfAdDto : IHaveCustomMapping
    {
        public string ServiceTypeName { get; set; }
        public string Price { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap< PaidService, PaidServicesOfAdDto>()
             .ForMember(d => d.ServiceTypeName , cfg => cfg.MapFrom(c => c.Service.ServiceType.Name))
             .ForMember(d => d.Price, cfg => cfg.MapFrom(c => c.Service.Price )) 
              ;    
         }

    }

}
