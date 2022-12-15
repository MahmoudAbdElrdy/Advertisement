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

namespace AuthApplication.Regions.Queries
{
    public class GetAllCountriesWithPaginationCommand : Paging, IRequest<PageList>
    {
    }
    public class GetAllCountriesWithPaginationCommandHandler : IRequestHandler<GetAllCountriesWithPaginationCommand, PageList>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCountriesWithPaginationCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PageList> Handle(GetAllCountriesWithPaginationCommand request, CancellationToken cancellationToken)
        {
            var query = _context.Set<Country>().AsQueryable();
            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.Name.Values.Contains(request.Filter));
            }

            return await query.ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                                      .ToPagedListAsync(request, cancellationToken);

        }
    }



}
