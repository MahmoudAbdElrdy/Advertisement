using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Complaint.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces;
using Common.Options;
using Infrastructure;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Common.Localization;
namespace AuthApplication.Complaint.Command
{
    public class CreateGeneralConfigurationCommand : IRequest<GeneralConfigurationDto>
    {
        public ValueTypeEnum ValueType { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        class Handler : IRequestHandler<CreateGeneralConfigurationCommand, GeneralConfigurationDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly INotificationService notificationService;
            private readonly ILocalizationProvider _localizationProvider;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, INotificationService notificationService, IImageService imageService, IOptions<AppSettings> systemSetting , ILocalizationProvider localizationProvider)
            {
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
                this.notificationService = notificationService;
                _localizationProvider = localizationProvider;
            }

            public async Task<GeneralConfigurationDto> Handle(CreateGeneralConfigurationCommand request, CancellationToken cancellationToken)
            {               
                GeneralConfiguration Complaint = new GeneralConfiguration()
                {
                    Name = request.Name,
                    Value = request.Value,//request.ClientId,
                    ValueType = request.ValueType,
                    Code = ""
                };

                _context.Set<GeneralConfiguration>().Add(Complaint);
                return _mapper.Map<GeneralConfigurationDto>(Complaint);

            }
        }
    }
}