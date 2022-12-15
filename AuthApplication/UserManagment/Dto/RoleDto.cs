using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Interfaces.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.UserManagment.Dto {
    public class RoleDto:IHaveCustomMapping {
        public string Name { get; set; }
        public string Id { get; set; }


        public void CreateMappings(Profile configuration) {
            configuration.CreateMap<Role, RoleDto>()
                          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) ;
        }
    }
}
