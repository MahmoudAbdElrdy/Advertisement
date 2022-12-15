using AuthDomain.Entities;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Regions.Dto
{
    public class CityDto : IHaveCustomMapping
    {
        public LocalizedData Name { get; set; }
        public LocalizedData CountryName { get; set; }
        public string Id { get; set; }
        public string CountryId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<City, CityDto>()
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name))
              .ForMember(d => d.CountryName, cfg => cfg.MapFrom(c => c.Country.Name))
              .ForMember(d => d.CountryId, cfg => cfg.MapFrom(c => c.CountryId))
              ;
        }
    }

}
