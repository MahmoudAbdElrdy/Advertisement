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
    public class ServicesDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string vendorName { get; set; }
        public decimal Price { get; set; }
        public LocalizedData Name { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServicesDto>()
              .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.ServiceType.Name))
              .ForMember(d => d.vendorName, cfg => cfg.MapFrom(c => c.User.FirstName)) ;
            
        }      

    }
}
