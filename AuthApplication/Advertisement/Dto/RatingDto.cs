using AuthDomain.Entities.Ads;
using AutoMapper;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Advertisement.Dto
{
  public  class RatingDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Rating, RatingDto>()
             
            
              .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
              .ForMember(d => d.AdId, cfg => cfg.MapFrom(c => c.AdId))
              .ForMember(d => d.ClientId, cfg => cfg.MapFrom(c => c.ClientId))
              .ForMember(d => d.RatingText, cfg => cfg.MapFrom(c => c.RatingText))
              .ForMember(d => d.RatingValue, cfg => cfg.MapFrom(c => c.RatingValue))
              ;
        }
    }

}
