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

namespace AuthApplication.Regions.Queries
{
    public class GetAllCountriesCommand : IRequest<List<CountryDto>>
    {
    }
    public class GetAllCountriesCommandHandler : IRequestHandler<GetAllCountriesCommand,List<CountryDto>>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCountriesCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CountryDto>> Handle(GetAllCountriesCommand request, CancellationToken cancellationToken)
        {
            var countryList = _context.Set<Country>().ToList();
            //if (!String.IsNullOrEmpty(request.Filter))
            //{
            //    query = query.Where(r => r.Name.Values.Contains(request.Filter));
            //}

            return await _context.Set<Country>().ProjectTo<CountryDto>(_mapper.ConfigurationProvider).ToListAsync();
             
        }
    }



}
