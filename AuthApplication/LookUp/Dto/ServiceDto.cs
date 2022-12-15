using AuthDomain.Entities;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.LookUp.Dto
{
    public class ServiceDto : IHaveCustomMapping
    {
        public string UserName { get; set; }
        public Decimal Price { get; set; }
        public string Id { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServiceDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.UserName, cfg => cfg.MapFrom(c => c.User.FullName))
              .ForMember(d => d.Price, cfg => cfg.MapFrom(c => c.Price))
              ;
        }
    }

}
