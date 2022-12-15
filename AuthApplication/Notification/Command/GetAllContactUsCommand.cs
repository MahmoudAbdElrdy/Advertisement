using AuthApplication.Notification.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.ContactUs.Command
{
 public   class GetAllContactUsCommand : Paging, IRequest<PageList<ContactUsDto>>
    {
        class Handler : IRequestHandler<GetAllContactUsCommand, PageList<ContactUsDto>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;


            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;

            }

            public async Task<PageList<ContactUsDto>> Handle(GetAllContactUsCommand request, CancellationToken cancellationToken)
            {
                var userId = auditService.UserId;
                var query = _context.Set<AuthDomain.Entities.ContactUs>().Where(x => x.ClientId == userId);

                var ContactUsPageList = await query.ProjectTo<ContactUsDto>(_mapper.ConfigurationProvider)
                  .ToPagedListAsync(request, cancellationToken);

                return ContactUsPageList;
            }
        }
    }
}