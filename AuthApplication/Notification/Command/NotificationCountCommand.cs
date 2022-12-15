using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Notifications.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Notification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace AuthApplication.Notifications.Command
{
    public class NotificationCountCommand :  IRequest<Result>
    {
        class Handler : IRequestHandler<NotificationCountCommand, Result>
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

            public async Task<Result> Handle(NotificationCountCommand request, CancellationToken cancellationToken)
            {
                var userId = auditService.UserId;
                var query =await _context.Set<AuthDomain.Entities.Notification.Notification>().Where(x => x.To == userId && !x.Read).CountAsync();

                return Result.Successed(query);
            }
        }
    }
}