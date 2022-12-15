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
  public class GetRequestTypeListCommand : IRequest<List<ServiceTypeDto>> { 
 
    class Handler : IRequestHandler<GetRequestTypeListCommand,List<ServiceTypeDto>> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;
      private readonly IAuditService _auditService; 
            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService) {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
      }

      public async Task<List<ServiceTypeDto>> Handle(GetRequestTypeListCommand request, CancellationToken cancellationToken) {

                var UserId = _auditService.UserId;
                var ListServies = _context.Services
                  .Include(a => a.User).Where(x=>x.UserId==UserId)
                  .Include(a => a.PaidServices)
                 .SelectMany(x=>x.PaidServices);
                var PaidServices = ListServies.Select(x => x.ServiceId).ToList();
                var countryList = _context.Set<ServiceType>().Where(x=>!PaidServices.Contains(x.Id)).ToList();
               

                return await _context.Set<ServiceType>().ProjectTo<ServiceTypeDto>(_mapper.ConfigurationProvider).ToListAsync();

            }
        }
  }


}
