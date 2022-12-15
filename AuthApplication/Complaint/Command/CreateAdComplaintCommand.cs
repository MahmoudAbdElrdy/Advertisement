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
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace AuthApplication.Complaint.Command
{
    public class CreateAdComplaintCommand : IRequest<AdComplaintDto>
    {
        public string ComplaintReason { get; set; }
        public string AdId { get; set; }
        public string ClientId { get; set; }
        class Handler : IRequestHandler<CreateAdComplaintCommand, AdComplaintDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
       

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                _context = context;
                _mapper = mapper;
                this.auditService = auditService;
            }

            public async Task<AdComplaintDto> Handle(CreateAdComplaintCommand request, CancellationToken cancellationToken)
            {

                AdComplaint Complaint = new AdComplaint() { 
                    AdId= request.AdId,
                    ClientId = auditService.UserId,//request.ClientId,
                    ComplaintReason = request.ComplaintReason,
                    IsComplaintSeen =false
                };


                await _context.Set<AdComplaint>().AddAsync(Complaint);
                return _mapper.Map<AdComplaintDto>(Complaint);

            }
        }
    }
}