using DTO;

namespace Domain.Entity;

public class MoodDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public MoodTypes Mood { get; set; }
    public MusicGenres Genre { get; set; }
    public string Description { get; set; }

    //references
    public Guid UserId { get; set; }
    public User User { get; set; }
}


