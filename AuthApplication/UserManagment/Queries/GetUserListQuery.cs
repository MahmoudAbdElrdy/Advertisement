using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Infrastructures;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AuthApplication.UserManagment.Queries {
  public class GetUserListQuery : Paging, IRequest<PageList> {
    class Handler : IRequestHandler<GetUserListQuery, PageList> {
      private readonly IAppDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PageList> Handle(GetUserListQuery request, CancellationToken cancellationToken) {
        var users = await _context.Set<User>().Protected()
          .AsQueryable()
          .Include(a=>a.Avatar)
          .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
          .ToPagedListAsync(request, cancellationToken);
               
         

        return users;
      }
    }
  }
}