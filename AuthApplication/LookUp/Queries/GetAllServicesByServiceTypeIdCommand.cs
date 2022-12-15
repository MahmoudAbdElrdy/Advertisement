using AuthApplication.LookUp.Dto;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces.Mapper;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.LookUp.Queries
{
    public class GetAllServicesByServiceTypeIdCommand : IRequest<List<ServiceDto>>
    {
        public string ServiceTypeId { get; set; }
    }
    public class GetAllServicesByServiceTypeIdCommandHandler : IRequestHandler<GetAllServicesByServiceTypeIdCommand, List<ServiceDto>>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllServicesByServiceTypeIdCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ServiceDto>> Handle(GetAllServicesByServiceTypeIdCommand request, CancellationToken cancellationToken)
        {
            var query = _context.Set<Service>().Include("User")
                .Where(c=>c.ServiceTypeId== request.ServiceTypeId)
                ;
            var services = await query.ToListAsync(cancellationToken);
            return _mapper.Map<List<ServiceDto>>(services);



        }
    }



}
