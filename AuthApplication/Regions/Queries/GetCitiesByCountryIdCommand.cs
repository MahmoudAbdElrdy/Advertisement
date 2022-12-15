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
    public class GetCitiesByCountryIdCommand : IRequest<List<CityDto>>
    {
        public string CountryId { get; set; }
    }
    public class GetCitiesByCountryIdCommandHandler : IRequestHandler<GetCitiesByCountryIdCommand, List<CityDto>>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetCitiesByCountryIdCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CityDto>> Handle(GetCitiesByCountryIdCommand request, CancellationToken cancellationToken)
        {
            var countryList = _context.Set<City>().Where(c=>c.CountryId== request.CountryId).ToList();
            //if (!String.IsNullOrEmpty(request.Filter))
            //{
            //    query = query.Where(r => r.Name.Values.Contains(request.Filter));
            //}

            return await _context.Set<City>().ProjectTo<CityDto>(_mapper.ConfigurationProvider).ToListAsync();
             
        }
    }



}
