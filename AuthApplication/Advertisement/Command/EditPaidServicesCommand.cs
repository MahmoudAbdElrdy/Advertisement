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
  public class EditPaidServicesCommand : IRequest<bool> {

        public string ServiceId { get; set; }
        public decimal Price { get; set; }


        class Handler : IRequestHandler<EditPaidServicesCommand, bool>
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

            public async Task<bool> Handle(EditPaidServicesCommand request, CancellationToken cancellationToken)
            {
                var service = _context.Services.FirstOrDefault(x => x.Id == request.ServiceId);
                try
                {
                    if (service != null)
                    {
                        service.Price = request.Price;
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                
                return false;
            }
        }
    }
}