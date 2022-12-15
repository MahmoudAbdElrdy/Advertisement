using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Complaint.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Complaint.Command
{

    public class GetGeneralConfigurationCommand : IRequest<GeneralConfigurationDto>
    {
        public long Id { get; set; }

        class Handler : IRequestHandler<GetGeneralConfigurationCommand, GeneralConfigurationDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralConfigurationDto> Handle(GetGeneralConfigurationCommand request, CancellationToken cancellationToken)
            {
                var query = await _context.Set<GeneralConfiguration>().FirstOrDefaultAsync(x => x.Id == request.Id);

                return _mapper.Map<GeneralConfigurationDto>(query);
            }
        }
    }
}