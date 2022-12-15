using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.UserManagment.Commands {
   public class AddRoleCommand :IRequest<Result> {
        public string Name { get; set; }
    }

    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Result> {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public AddRoleCommandHandler(IAppDbContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddRoleCommand request, CancellationToken cancellationToken) {

            var role = new Role(request.Name) {
                NormalizedName = request.Name.ToUpper()
            };

            await  _context.Set<Role>().AddAsync(role,cancellationToken);

            return Result.Successed(_mapper.Map<RoleDto>(role));
        }
    }
}
