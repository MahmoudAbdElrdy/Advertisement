using AuthApplication.UserManagment.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace AuthApplication.Auth.Validatores {
   public class UserValidator : AbstractValidator<AddUserCommand> {

        public UserValidator() {
            this.RuleFor(r => r.UserName).NotEmpty();
            this.RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("A valid Email Address is Required");
            this.RuleFor(r => r.Password).NotEmpty();
            this.RuleFor(r => r.PhoneNumber).NotEmpty().MaximumLength(11);
              
        }


    }
}
