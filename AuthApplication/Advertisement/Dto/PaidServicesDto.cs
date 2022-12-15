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
    public class PaidServicesDto : IHaveCustomMapping
    {
        public string ServiceTypeId { get; set; }
        public string Price { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PaidServicesDto, Service>();
        }

    }

}
