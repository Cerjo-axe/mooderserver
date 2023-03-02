using Domain.Entity;
using DTO;
using FluentValidation;

namespace Service.Validators;

public class UserValidate : AbstractValidator<RegisterDTO>
{
    public void UserValidator()
    {
        RuleFor(c=>c.UserName).NotEmpty().Length(10, 150);

        RuleFor(c=>c.Email).NotEmpty().EmailAddress();
        RuleFor(c=>c.Password).NotEmpty().MaximumLength(8);
    }
}
