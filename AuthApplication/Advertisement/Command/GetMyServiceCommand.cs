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
  public class GetMyServiceCommand : IRequest<List<GetPaidServicesDto>> { 

        class Handler : IRequestHandler<GetMyServiceCommand, List<GetPaidServicesDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly IImageService _imageService;
            AppSettings _appSettings;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                _appSettings = systemSetting.Value;
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
                this._imageService = imageService;
            }

            public async Task<List<GetPaidServicesDto>> Handle(GetMyServiceCommand request, CancellationToken cancellationToken)
            {
              var UserId = auditService.UserId; 
             List<GetPaidServicesDto> services =new List<GetPaidServicesDto>();
             var mySevices =await  _context.Services.Include(a => a.ServiceType).Where(c => c.UserId == UserId).ToListAsync();
             services =  _mapper.Map<List<GetPaidServicesDto>>(mySevices);
              return services; 
            }
        }
    }
}