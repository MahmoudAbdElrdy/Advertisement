using AuthApplication.LookUp.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Interfaces.Mapper;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.LookUp.Commands {
  public class GetServiceTypeListCommand : IRequest<List<ServiceTypeDto>> {
 
    class Handler : IRequestHandler<GetServiceTypeListCommand,List<ServiceTypeDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;
      private readonly IAuditService _auditService; 
            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService) {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
      }

      public async Task<List<ServiceTypeDto>> Handle(GetServiceTypeListCommand request, CancellationToken cancellationToken) {

                var UserId = _auditService.UserId;
                var ListServies = _context.Services.Where(x=>x.UserId==UserId).Select(x=>x.ServiceTypeId).ToList();
               var countryList = _context.Set<ServiceType>().Where(x=>!ListServies.Contains(x.Id)).ToList();
               

                return await _context.Set<ServiceType>().Where(x => !ListServies.Contains(x.Id)).ProjectTo<ServiceTypeDto>(_mapper.ConfigurationProvider).ToListAsync();

            }
        }
  }


}
