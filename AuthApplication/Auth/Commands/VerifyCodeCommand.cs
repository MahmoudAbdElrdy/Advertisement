using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Auth.Commands {
  public class VerifyCodeCommand : IRequest<Result> {
    public int Code { get; set; }
    public string Username { get; set; }

    class Hander : IRequestHandler<VerifyCodeCommand, Result> {
      private readonly IAppDbContext _context;

      public Hander(IAppDbContext context) {
        _context = context;
      }

      public async Task<Result> Handle(VerifyCodeCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);
        if (user == null)
          throw new ApiException(ApiExeptionType.NotFound, "User not found ");
        var userCodes = await _context.UserCodes
          .Where(c => c.Code == request.Code && c.UserId == user.Id)
          .OrderByDescending(c => c.CreateDate)
          .FirstOrDefaultAsync(cancellationToken);
        
        if (userCodes == null)
          throw new ApiException(ApiExeptionType.NotFound, "This code is not found or expire ");
        
        if ((DateTime.Now - userCodes.CreateDate.ToLocalTime()).Minutes>5)
          throw new ApiException(ApiExeptionType.NotFound, "This code is expire ");
        
        return Result.Successed();
        
        
      }
    }
  }
}