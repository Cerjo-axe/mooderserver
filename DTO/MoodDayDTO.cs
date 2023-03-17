namespace DTO;
public class MoodDayDTO
{
    public DateTime Date { get; set; }
    public MoodTypes Mood { get; set; }
    public MusicGenres MusicGenre { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
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
