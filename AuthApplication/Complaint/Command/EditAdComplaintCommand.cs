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
    public class EditAdComplaintCommand : IRequest<AdComplaintDto>
    {
        public string Id { get; set; }
        public string ComplaintReason { get; set; }
        public string ComplaintReasonReplay { get; set; }
        public bool? IsComplaintSeen { get; set; }

        class Handler : IRequestHandler<EditAdComplaintCommand, AdComplaintDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AdComplaintDto> Handle(EditAdComplaintCommand request, CancellationToken cancellationToken)
            {
                var adComplaint = await _context.Set<AdComplaint>().FindAsync(request.Id);

                adComplaint.ComplaintReason = request.ComplaintReason;
                adComplaint.ComplaintReasonReplay = request.ComplaintReasonReplay;
                adComplaint.IsComplaintSeen = request.IsComplaintSeen;

                 _context.Set<AdComplaint>().Update(adComplaint);
                return _mapper.Map<AdComplaintDto>(adComplaint);
            }
        }
    }


}
