using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.UserManagment.Commands {
  public class EditRoleCommand :IRequest<Result>{
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Result> {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public EditRoleCommandHandler(IAppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(EditRoleCommand request, CancellationToken cancellationToken) {

            var role = await _context.Set<Role>().FindAsync(request.Id);
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();


            _context.Set<Role>().Update(role);

            return Result.Successed(_mapper.Map<RoleDto>(role));
        }
    }
}
