using System;
using System.Linq;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces.Mapper;

namespace AuthApplication.Complaint.Dto
{
    public class AdComplaintDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string ComplaintReason { get; set; }
        public string ComplaintReasonReplay { get; set; }
        public bool? IsComplaintSeen { get; set; }
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string AdOwnerName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AdComplaint, AdComplaintDto>()
           .ForMember(d => d.ClientName, cfg => cfg.MapFrom(c => c.Client!=null? c.Client.FullName:""))
           .ForMember(d => d.AdOwnerName, cfg => cfg.MapFrom(c => c.Ad!=null? c.Ad.SpaceInfo.Client.FullName:""));
        }
    }
}