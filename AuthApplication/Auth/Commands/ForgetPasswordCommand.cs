using System;
using System.Threading;
using System.Threading.Tasks;
using AuthDomain.Entities;
using Common;
using Infrastructure;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Auth.Commands {
    public class ForgetPasswordCommand : IRequest<Result> {
        public string Username { get; set; }
        class Handler : IRequestHandler<ForgetPasswordCommand, Result> {
            private readonly IIdentityService _identityService;
            private readonly IAppDbContext _context;

            public Handler(IIdentityService identityService,IAppDbContext context) {
                _identityService = identityService;
                _context = context;
            }
            public async Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken) {
                var random = new Random();
                var code = random.Next(9999);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);
                if (user == null)
                    throw new ApiException(ApiExeptionType.NotFound, "User not found ");

                var userCode = new UserCode() {
                    Code = code,
                    UserId = user.Id,
                    CreateDate = DateTime.UtcNow
                };
                await _context.UserCodes.AddAsync(userCode, cancellationToken);

                return Result.Successed(new{Code=code,ValidTo=DateTime.Now.AddMinutes(5)});
            }
        }
    }
}
