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
    public class EditCityCommand : IRequest<Result>
    {
        public string CountryId { get; set; } 
        public LocalizedData Name { get; set; }
        public string Id { get; set; }
    }

    public class EditCityCommandHandler : IRequestHandler<EditCityCommand, Result>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public EditCityCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {

            var city = await _context.Set<City>().FindAsync(request.Id);
            city.Name = request.Name;  
            _context.Set<City>().Update(city);

            return Result.Successed(_mapper.Map<CityDto>(city));
        }
    } 
}
 