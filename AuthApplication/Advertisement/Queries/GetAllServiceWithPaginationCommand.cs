using AuthApplication.LookUp.Dto;
using AuthDomain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.Advertisement.Queries
{
 public   class GetAllServiceWithPaginationCommand : Paging, IRequest<PageList>
    {
    }
    public class GetAllServiceWithPaginationCommandHandler : IRequestHandler<GetAllServiceWithPaginationCommand, PageList>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllServiceWithPaginationCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PageList> Handle(GetAllServiceWithPaginationCommand request, CancellationToken cancellationToken)
        {
            var query = _context.Set<ServiceType>().AsQueryable();
            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.Name.Values.Contains(request.Filter));
            }

            return await query.ProjectTo<ServiceTypeDto>(_mapper.ConfigurationProvider)
                                      .ToPagedListAsync(request, cancellationToken);

        }
    }



}
