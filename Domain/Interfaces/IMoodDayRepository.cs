using Domain.Entity;

namespace Domain.Interfaces;

public interface IMoodDayRepository
{
    Task Add(MoodDay obj);
    void Update(MoodDay obj);
    void Delete(MoodDay day);
    Task<IEnumerable<MoodDay>> GetByWeek(Guid id, DateTime fromdate);
    Task<IEnumerable<MoodDay>> GetByMonth(Guid id, DateTime fromdate);
}
