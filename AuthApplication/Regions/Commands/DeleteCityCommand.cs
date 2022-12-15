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
    public class DeleteCityCommand : IRequest<CityDto>
    {
        public string Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteCityCommand, CityDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteUserCommandHandler(IAppDbContext context, IMapper mapper)
        { _context = context;
            _mapper = mapper;
        }
        public async Task<CityDto> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.Set<City>().FindAsync(request.Id);
            if (city == null)
            {
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<City>().Remove(city);
            return _mapper.Map<CityDto>(city);
        }
    }

    }
 