using AuthDomain.Entities;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.LookUp.Dto
{
    public class ServiceTypeDto : IHaveCustomMapping
    {
        public LocalizedData Name { get; set; }
        public LocalizedData Description { get; set; }
        public string Id { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ServiceType, ServiceTypeDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name))
              .ForMember(d => d.Description, cfg => cfg.MapFrom(c => c.Description))
              ;
        }
    }

}
