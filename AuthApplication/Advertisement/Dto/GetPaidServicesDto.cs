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
    public class GetPaidServicesDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string ServiceTypeId { get; set; }
        public decimal Price { get; set; }
        public LocalizedData ServiceName { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GetPaidServicesDto,Service> ().ReverseMap()
            .ForMember(d => d.ServiceName,  cfg => cfg.MapFrom(c => c.ServiceType.Name));
        }
    }

}
