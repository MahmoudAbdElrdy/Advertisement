using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command {
  public class GetAdServicesCommand : IRequest<List<PaidServicesOfAdDto>> {
        public string AdIntervalId { get; set; }
        class Handler : IRequestHandler<GetAdServicesCommand, List<PaidServicesOfAdDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
             AppSettings _appSettings;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService,   IOptions<AppSettings> systemSetting)
            {
                _appSettings = systemSetting.Value;
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
             }

            public async Task<List<PaidServicesOfAdDto>> Handle(GetAdServicesCommand request, CancellationToken cancellationToken)
            {
              List<PaidServicesOfAdDto> services =new List<PaidServicesOfAdDto>();
             var mySevices = await _context.PaidServices.Where(c => c.AdIntervalId == request.AdIntervalId).ToListAsync(); 
             services =  _mapper.Map<List<PaidServicesOfAdDto>>(mySevices);
              return services; 
            }
        }
    }
}