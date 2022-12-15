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

namespace AuthApplication.Regions.Commands {
  public class AddCityCommand : IRequest<CityDto> {
        public string CountryId { get; set; }
        public LocalizedData Name { get; set; }

    class Handler : IRequestHandler<AddCityCommand, CityDto> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }

            public async Task<CityDto> Handle(AddCityCommand request, CancellationToken cancellationToken)
            {
                var city = new City()
                {
                    Name = request.Name,
                    CountryId = request.CountryId
                }; 
                 await _context.Set<City>().AddAsync(city, cancellationToken);
                 return _mapper.Map<CityDto>(city);
            }
        }
    }
  }

