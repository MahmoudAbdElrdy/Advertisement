using AuthApplication.LookUp.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AutoMapper;
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
  public class CreateServiceCommand : IRequest<ServiceTypeDto> {
    public LocalizedData Name { get; set; }
     public LocalizedData Description { get; set; }
        class Handler : IRequestHandler<CreateServiceCommand, ServiceTypeDto> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<ServiceTypeDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken) {
        var service = new ServiceType() {
          Name = request.Name,
          Description = request.Description,
        };
                service.Id= Guid.NewGuid().ToString();
                await _context.Set<ServiceType>().AddAsync(service, cancellationToken);
        return _mapper.Map<ServiceTypeDto>(service);
      }
    }
  }


}
