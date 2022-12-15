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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.LookUp.Queries
{
    public class GetAllServicesWithPaginationCommand : Paging, IRequest<PageList>
    {
    }
    public class GetAllServicesWithPaginationCommandHandler : IRequestHandler<GetAllServicesWithPaginationCommand, PageList>
    { 
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllServicesWithPaginationCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PageList> Handle(GetAllServicesWithPaginationCommand request, CancellationToken cancellationToken)
        {
            var query = _context.Set<ServiceType>().AsQueryable();
            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.Name.Values.Contains(request.Filter));
            }

            return await query.ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                                      .ToPagedListAsync(request, cancellationToken);

        }
    }



}
