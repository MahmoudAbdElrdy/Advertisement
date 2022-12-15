using AuthApplication.UserManagment.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Auth.Validatores {
   public class RoleValidator : AbstractValidator<AddRoleCommand> {

        public RoleValidator() {
            this.RuleFor(r => r.Name).NotEmpty();

        }
    }
}
