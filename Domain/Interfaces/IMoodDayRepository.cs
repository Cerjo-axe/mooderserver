using Domain.Entity;

namespace Domain.Interfaces;

public interface IMoodDayRepository
{
    Task Add(MoodDay obj);
    void Update(MoodDay obj);
    void Delete(int id);
    Task<IEnumerable<MoodDay>> GetByWeek();
    Task<IEnumerable<MoodDay>> GetByMonth();
}
