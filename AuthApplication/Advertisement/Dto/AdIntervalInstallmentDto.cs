using System;
using System.Collections.Generic;
using System.Linq;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Advertisement.Dto
{
    public class AdIntervalInstallmentDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public decimal Amount { get; set; } 
        public bool IsPaid { get; set; } 
        public DateTime DueDate { get; set; }
   
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Installment, AdIntervalInstallmentDto>()
               ;
        }  
    }
}
