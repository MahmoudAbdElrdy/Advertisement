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
  public class DeleteRoleCommand : IRequest<RoleDto> {

        public string Id { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, RoleDto> {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;


        public DeleteRoleCommandHandler(IAppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken) {

            var role = await _context.Set<Role>().FindAsync(request.Id);
            if (role == null) {
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<Role>().Remove(role);
            return _mapper.Map<RoleDto>(role);

        }
    }
}
