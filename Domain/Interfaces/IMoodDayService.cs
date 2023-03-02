using Domain.Entity;
using DTO;

namespace Domain.Interfaces;

public interface IMoodDayService
{
    Task<MoodDayDTO> Add(MoodDayDTO obj);
    MoodDayDTO Update(MoodDayDTO obj);
    void Delete(int id);
    Task<IEnumerable<MoodDayDTO>> GetAll();
    MoodDayDTO Get(int id);

}
