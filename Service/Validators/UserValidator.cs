using Domain.Entity;
using DTO;
using FluentValidation;

namespace Service.Validators;

public class UserValidator : AbstractValidator<RegisterDTO>
{
    public UserValidator()
    {
        RuleFor(c=>c.UserName).NotEmpty().Length(10, 150).WithMessage("InvalidUserName");

        RuleFor(c=>c.Email).NotEmpty().EmailAddress();
        RuleFor(c=>c.Password).NotEmpty().MinimumLength(8);
    }
}
