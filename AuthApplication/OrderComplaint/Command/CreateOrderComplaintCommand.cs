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
    public class CreateOrderComplaintCommand : IRequest<OrderComplaintDto>
    {
        public string ComplaintReason { get; set; }
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public ComplainType ComplainType { get; set; }

        class Handler : IRequestHandler<CreateOrderComplaintCommand, OrderComplaintDto>
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

            public async Task<OrderComplaintDto> Handle(CreateOrderComplaintCommand request, CancellationToken cancellationToken)
            {
                Client toUser =new Client();
                string UserId= auditService.UserId;
                if (request.ComplainType == ComplainType.ads)
                {
                    toUser = _context.AdIntervals.Include(x => x.Client).FirstOrDefault(x => x.Id == request.OrderId)?.Client;
                }
                else {
                    var service= _context.AdIntervals.Include(x => x.Client).Include(x=>x.PaidServices).ThenInclude(x => x.Service).
                        FirstOrDefault(x =>  x.PaidServices.Any(c => c.AdInterval.ClientId == UserId && c.ServiceId==request.OrderId));
                    toUser = service?.Client;
                    request.OrderId = service?.Id;
                }
                OrderComplaint Complaint = new OrderComplaint()
                {
                    OrderId = request.OrderId,
                    ClientId = UserId,//request.ClientId,
                    ComplaintReason = request.ComplaintReason,
                    IsComplaintSeen = false,
                    ComplainType=request.ComplainType
                };

                _context.Set<OrderComplaint>().Add(Complaint);
                await notificationService.Save(new AuthDomain.Entities.Notification.Notification()
                {
                    BodyAr = request.ComplaintReason,
                    BodyEn = request.ComplaintReason,
                    //  Subject = string.Format("Complain In Order Number {0}", request.OrderId)
                    SubjectEn = string.Format(_localizationProvider.Localize("test","en"), request.OrderId),
                    SubjectAr = string.Format(_localizationProvider.Localize("test", "ar"), request.OrderId),
                    From = auditService.UserId,
                    To= toUser.Id                    
                }, toUser.WebToken);
                return _mapper.Map<OrderComplaintDto>(Complaint);

            }
        }
    }
}