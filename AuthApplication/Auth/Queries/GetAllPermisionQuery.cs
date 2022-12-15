using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthDomain.Entities.Auth;
using System.Linq;
using AuthDomain.Enums.Roles;
using Infrastructure;
using Infrastructure.Interfaces;

namespace AuthApplication.Auth.Queries {
    public class GetAllPermisionQuery : IRequest<Result> {

    class Handler : IRequestHandler<GetAllPermisionQuery, Result> {
        private readonly IPermissionService service;

        public Handler(IPermissionService service) {
            this.service = service;
        }
        public async Task<Result> Handle(GetAllPermisionQuery request, CancellationToken cancellationToken) {

            return Result.Successed(service.GetPermissions());

        }
    }
    }

}
