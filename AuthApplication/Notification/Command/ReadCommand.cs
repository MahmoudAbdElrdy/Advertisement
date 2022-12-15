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
    public class ReadCommand : IRequest<Result>
    {
        public int Id { get; set; }
        class Handler : IRequestHandler<ReadCommand, Result>
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

            public async Task<Result> Handle(ReadCommand request, CancellationToken cancellationToken)
            {
                var query =await _context.Set<AuthDomain.Entities.Notification.Notification>().FirstOrDefaultAsync(x => x.Id == request.Id);
                if (query==null)
                { throw new ApiException(ApiExeptionType.NotFound); }
                //query.Read = true;
                await _context.SaveChangesAsync();
                return Result.Successed();
            }
        }
    }
}