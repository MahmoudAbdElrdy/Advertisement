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

namespace AuthApplication.Regions.Commands {
    public class EditCountryCommand : IRequest<Result>
    {
        public LocalizedData Name { get; set; }
        public string Id { get; set; }
    }

    public class EditCountryCommandHandler : IRequestHandler<EditCountryCommand, Result>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public EditCountryCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(EditCountryCommand request, CancellationToken cancellationToken)
        {

            var country = await _context.Set<Country>().FindAsync(request.Id);
            country.Name = request.Name;
 

            _context.Set<Country>().Update(country);

            return Result.Successed(_mapper.Map<CountryDto>(country));
        }
    }


    //public async Task<CityDto> Handle(EditCityCommand request, CancellationToken cancellationToken)
    //{
    //    var city = new City()
    //    {
    //        Name = request.Name
    //    }; 
    //     await _context.Set<City>().AddAsync(city, cancellationToken);
    //     return _mapper.Map<CityDto>(city);
    //}





}
 