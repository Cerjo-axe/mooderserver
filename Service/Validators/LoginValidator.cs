using Domain.Entity;
using DTO;
using FluentValidation;

public class LoginValidator : AbstractValidator<LoginDTO>
{
    public LoginValidator()
    {
        RuleFor(c=>c.Email).NotEmpty().EmailAddress();
        RuleFor(c=>c.Password).NotEmpty().MinimumLength(8);
    }
}
