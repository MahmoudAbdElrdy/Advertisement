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
  public class CreateCountryCommand : IRequest<CountryDto> {
    public LocalizedData Name { get; set; }

    class Handler : IRequestHandler<CreateCountryCommand, CountryDto> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<CountryDto> Handle(CreateCountryCommand request, CancellationToken cancellationToken) {
        var country = new Country() {
          Name = request.Name
        };

        await _context.Set<Country>().AddAsync(country, cancellationToken);
        return _mapper.Map<CountryDto>(country);
      }
    }
  }


}
