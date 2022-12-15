using AuthApplication.Notification.Dto;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.Notification.Command
{
   public class DeleteContactUsCommand : IRequest<ContactUsDto>
    {
        public string Id { get; set; }
    }
    public class DeleteContactUsCommandHandler : IRequestHandler<DeleteContactUsCommand, ContactUsDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteContactUsCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ContactUsDto> Handle(DeleteContactUsCommand request, CancellationToken cancellationToken)
        {
            var ContactUs = await _context.Set<AuthDomain.Entities.ContactUs>().FindAsync(request.Id);
            if (ContactUs == null)
            {
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<AuthDomain.Entities.ContactUs>().Remove(ContactUs);
            return _mapper.Map<ContactUsDto>(ContactUs);
        }
    }
}
