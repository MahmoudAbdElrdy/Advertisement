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
   public class DeleteUserCommand : IRequest<UserDto> {

        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto> {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;


        public DeleteUserCommandHandler(IAppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {

            var user = await _context.Set<User>().FindAsync(request.Id);
            if (user == null){
                throw new ApiException(ApiExeptionType.NotFound);
            }
            _context.Set<User>().Remove(user);
            return _mapper.Map<UserDto>(user);
        }

       
    }
}
