using AuthDomain.Entities;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Regions.Dto
{
    public class CountryDto : IHaveCustomMapping
    {
        public LocalizedData Name { get; set; }
        public string Id { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Country, CountryDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name))
              ;
        }
    }

}
