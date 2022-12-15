using AuthDomain.Entities;
using AutoMapper;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Notification.Dto
{
  public  class ContactUsDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ResponesAdmin { get; set; }
        public string Notes { get; set; }
        public bool? IsContact { get; set; }
        public string ClientId { get; set; }
       

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ContactUsDto, AuthDomain.Entities.ContactUs>().ReverseMap();
                                        
        }
    }
}
