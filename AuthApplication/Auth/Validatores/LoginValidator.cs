using AuthApplication.Auth.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApplication.Auth.Validatores {
   public class LoginValidator :AbstractValidator<LoginCommand> {

        public LoginValidator() {
            this.RuleFor(r => r.Username).NotNull();
            this.RuleFor(r => r.Password).NotEmpty();
        }

    }
  
}
