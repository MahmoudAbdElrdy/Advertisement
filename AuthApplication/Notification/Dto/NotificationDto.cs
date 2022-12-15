using System;
using System.Linq;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Notification;
using AuthDomain.Enums;
using AuthDomain.Enums.Notifications;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Notifications.Dto
{
    public class NotificationDto : IHaveCustomMapping
    {
        public int Id { get; set; }       
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; } = false;
        public DateTime CreatedDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap< NotificationDto, AuthDomain.Entities.Notification.Notification>().ReverseMap()
                                          .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.SubjectAr))
                                          .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.BodyAr))

                .AfterMap<LocalizationMappingAction<AuthDomain.Entities.Notification.Notification, NotificationDto>>();
        }
    }
}