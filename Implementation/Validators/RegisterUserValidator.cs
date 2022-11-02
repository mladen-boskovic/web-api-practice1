using Application.DTOs;
using DataAccess.EntityFramework;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(WebApiPracticeContext context)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email format")
                .Must(x => !context.Users.Any(u => u.Email == x)).WithMessage("Email {PropertyValue} already exists");

            var usernameRegex = "^(?=[a-zA-Z0-9._]{5,20}$)(?!.*[_.]{2})[^_.].*[^_.]$";
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(5).WithMessage("Username should contain at least 5 characters")
                .MaximumLength(20).WithMessage("Username should not contain more than 20 characters")
                .Matches(usernameRegex)
                .WithMessage("Invalid Username format")
                .Must(x => !context.Users.Any(u => u.Username == x)).WithMessage("Username {PropertyValue} already exists");

            var firstLastNameRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("FirstName is required")
                .Matches(firstLastNameRegex).WithMessage("Invalid FirstName format");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("LastName is required")
                .Matches(firstLastNameRegex).WithMessage("Invalid LastName format");

            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required")
                .Matches(passwordRegex).WithMessage("Password should contain at least 8 characters, one uppercase letter, one lowercase letter, number and special character");
        }
    }
}
