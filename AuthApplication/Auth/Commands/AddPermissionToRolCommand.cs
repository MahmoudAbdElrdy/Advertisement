using AuthDomain.Entities.Auth;
using AuthDomain.Enums.Roles;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Common.Attributes;

namespace AuthApplication.Auth.Commands {
    public class AddPermissionToRolCommand : IRequest<Result> {
        public string Permission { get; set; }
        public string RoleId { get; set; }


    }

    public class AddPermissionToRolCommandHandler : IRequestHandler<AddPermissionToRolCommand, Result> {
        private readonly IPermissionService _service;
        private readonly IAppDbContext context;


        public AddPermissionToRolCommandHandler(IPermissionService service, IAppDbContext context) {
            _service = service;
            this.context = context;

        }

        public async Task<Result> Handle(AddPermissionToRolCommand request, CancellationToken cancellationToken) {


            var role = await context.Set<Role>().FirstOrDefaultAsync(r => r.Id == request.RoleId);
            await _service.AddPermissionsToRole(role, request.Permission);

            return Result.Successed();
        }


    }




}
