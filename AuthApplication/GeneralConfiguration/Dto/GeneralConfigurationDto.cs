using System;
using System.Linq;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Complaint.Dto
{

    public class GeneralConfigurationDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public ValueTypeEnum ValueType { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GeneralConfiguration, GeneralConfigurationDto>();
        }
    }
}