using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class User:IdentityUser
{
    public ICollection<MoodDay> MoodDays {get; set;}
}
