using AuthApplication.LookUp.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AutoMapper;
using Common;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.LookUp.Commands {
    public class DeleteServiceCommand : IRequest<ServiceTypeDto>
    {
        public string Id { get; set; }
    }
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ServiceTypeDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteServiceCommandHandler(IAppDbContext context, IMapper mapper)
        { _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceTypeDto> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Set<ServiceType>().FindAsync(request.Id);
            if (service == null)
            {
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<ServiceType>().Remove(service);
            return _mapper.Map<ServiceTypeDto>(service);
        }
    }

    }
 