using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Interfaces.Mapper;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Complaint.Dto;

namespace AuthApplication.Complaint.Commands
{

    public class EditGeneralConfigurationCommand : IRequest<GeneralConfigurationDto>
    {
        public long Id { get; set; }
        public ValueTypeEnum ValueType { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        class Handler : IRequestHandler<EditGeneralConfigurationCommand, GeneralConfigurationDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralConfigurationDto> Handle(EditGeneralConfigurationCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.Set<GeneralConfiguration>().FindAsync(request.Id);

                item.Value = request.Value;
               
                 _context.Set<GeneralConfiguration>().Update(item);
                return _mapper.Map<GeneralConfigurationDto>(item);
            }
        }
    }


}
