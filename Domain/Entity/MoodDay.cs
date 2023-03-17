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

public enum MoodTypes
{
    Happy,
    Sad,
    Angry,
    Anxious,
    Neutral
}
public enum MusicGenres
{
    Rock,
    Metal,
    Pop,
    Country,
    Soul,
    Sertanejo,
    Eletrônica,
    Clássica,
    HipHop,
    Regggae,
    Jazz,
    Blues,
    Funk,
    Folk,
    Indie
}
