using Domain.Entity;
using FluentValidation;

namespace Service.Validators;

public class MoodDayValidate : AbstractValidator<MoodDay>
{
    public void MoodValidator()
    {
        RuleFor(c=>c.Id);
    }
}
