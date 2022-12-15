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
    public class EditServiceCommand : IRequest<Result>
    {
         public LocalizedData Name { get; set; }
        public LocalizedData Description { get; set; }
        public string Id { get; set; }
    }

    public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand, Result>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public EditServiceCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(EditServiceCommand request, CancellationToken cancellationToken)
        {

            var service = await _context.Set<ServiceType>().FindAsync(request.Id);
            service.Name = request.Name;
            service.Description = request.Description;
            _context.Set<ServiceType>().Update(service);
            
            return Result.Successed(_mapper.Map<ServiceTypeDto>(service));
        }
    } 
}
 