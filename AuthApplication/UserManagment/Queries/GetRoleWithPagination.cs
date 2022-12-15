using AuthApplication.UserManagment.Dto;
using AuthDomain.Entities.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Extensions;
using Common.Infrastructures;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace AuthApplication.UserManagment.Queries {
  public class GetRoleWithPagination : Paging, IRequest<PageList>{

    }

    public class GetRoleWithPaginationHandler : IRequestHandler<GetRoleWithPagination, PageList> {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetRoleWithPaginationHandler(IAppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PageList> Handle(GetRoleWithPagination request, CancellationToken cancellationToken) {
            var query =  _context.Set<Role>().AsQueryable();
            if (!String.IsNullOrEmpty(request.Filter))
            {
                 query = query.Where(r => r.Name.Contains(request.Filter)) ;
            }
           
            return await  query.ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                                      .ToPagedListAsync(request, cancellationToken);

        }
    }
}
