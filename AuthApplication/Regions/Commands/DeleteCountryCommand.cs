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
    public class DeleteCountryCommand : IRequest<CountryDto>
    {
        public string Id { get; set; }
    }
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, CountryDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteCountryCommandHandler(IAppDbContext context, IMapper mapper)
        { _context = context;
            _mapper = mapper;
        }
        public async Task<CountryDto> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.Set<Country>().FindAsync(request.Id);
            if (city == null)
            {
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<Country>().Remove(city);
            return _mapper.Map<CountryDto>(city);
        }
    }

    }
 