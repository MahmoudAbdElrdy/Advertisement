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
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command {
  public class AddPaidServicesCommand : IRequest<bool> {
         
        public List<PaidServicesDto> PaidServices { get; set; }


        class Handler : IRequestHandler<AddPaidServicesCommand, bool>
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

            public async Task<bool> Handle(AddPaidServicesCommand request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId;

             List<Service> services =new List<Service>();

             services =  _mapper.Map<List<Service>>(request.PaidServices);

             services.ForEach(c=>c.UserId = auditService.UserId);
              _context.Services.AddRange(services);

                return true;
            
            }
        }
    }
}