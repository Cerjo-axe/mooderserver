using Domain.Entity;

namespace Domain.Interfaces;

public interface IMoodDayRepository
{
    Task Add(MoodDay obj);
    void Update(MoodDay obj);
    void Delete(int id);
    Task<IEnumerable<MoodDay>> GetAll();
    Task<MoodDay> Get(int id);
}
