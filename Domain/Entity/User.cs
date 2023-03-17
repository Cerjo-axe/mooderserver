using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class User:IdentityUser
{
    public IEnumerable<MoodDay>? MoodDays {get; set;}
}
