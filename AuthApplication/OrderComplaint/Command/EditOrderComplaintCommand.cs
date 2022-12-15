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
using Common.Localization;
using Infrastructure;

namespace AuthApplication.Complaint.Commands
{
    public class EditOrderComplaintCommand : IRequest<OrderComplaintDto>
    {
        public string Id { get; set; }
        public string ComplaintReason { get; set; }
        public string ComplaintReasonReplay { get; set; }
        public bool? IsComplaintSeen { get; set; }

        class Handler : IRequestHandler<EditOrderComplaintCommand, OrderComplaintDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILocalizationProvider _localizationProvider;
            private readonly INotificationService notificationService;
            private readonly IAuditService auditService;


            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting, ILocalizationProvider localizationProvider, INotificationService notificationService)
            {
                _context = context;
                _mapper = mapper;
                _localizationProvider = localizationProvider;
                this.notificationService = notificationService;
                this.auditService = auditService;
            }

            public async Task<OrderComplaintDto> Handle(EditOrderComplaintCommand request, CancellationToken cancellationToken)
            {
                var adComplaint = await _context.Set<OrderComplaint>().FindAsync(request.Id);

                if (adComplaint!=null)
                {
                    adComplaint.ComplaintReason = request.ComplaintReason;
                    var user = _context.Users.FirstOrDefault(x => x.Id == adComplaint.ClientId);

                    if (string.IsNullOrEmpty(adComplaint.ComplaintReasonReplay) && string.IsNullOrEmpty(request.ComplaintReasonReplay) && user != null)
                    {

                        await notificationService.Save(new AuthDomain.Entities.Notification.Notification()
                        {
                            BodyAr = request.ComplaintReasonReplay,
                            BodyEn = request.ComplaintReasonReplay,
                            //  Subject = string.Format("Complain In Order Number {0}", request.OrderId)
                            SubjectEn = string.Format(_localizationProvider.Localize("ComplainReplay", "en"), adComplaint.OrderId),
                            SubjectAr = string.Format(_localizationProvider.Localize("ComplainReplay", "ar"), adComplaint.OrderId),
                            From = auditService.UserId,
                            To = user.Id
                        }, user.WebToken);
                    }
                    adComplaint.ComplaintReasonReplay = request.ComplaintReasonReplay;
                    adComplaint.IsComplaintSeen = request.IsComplaintSeen;
                    _context.Set<OrderComplaint>().Update(adComplaint);
                }
                return _mapper.Map<OrderComplaintDto>(adComplaint);
            }
        }
    }


}
