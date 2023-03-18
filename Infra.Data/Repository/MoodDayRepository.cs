using Domain.Entity;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class MoodDayRepository : IMoodDayRepository
{

    protected readonly AppDbContext _context;

    public MoodDayRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(MoodDay obj)
    {
        try
        {
            await _context.Set<MoodDay>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async void Delete(MoodDay day)
    {
        _context.Set<MoodDay>().Remove(day);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<MoodDay>> GetByWeek(Guid id, DateTime fromdate)
    {
        var dateonly = fromdate.Date;
        var queryable = _context.MoodDays.Where(x=> x.Date.Date >= dateonly);
        queryable = queryable.Where(x=>x.UserId == id);
        return await queryable.ToListAsync();
    }

    public async Task<IEnumerable<MoodDay>> GetByMonth(Guid id, DateTime fromdate)
    {
        var dateonly = fromdate.Date;
        var queryable = _context.MoodDays.Where(x=> x.Date.Date >= dateonly);
        queryable = queryable.Where(x=>x.UserId == id);
        return await queryable.ToListAsync();
    }
    public async Task<MoodDay> GetByDay(Guid id, DateTime fromdate)
    {
        var dateonly = fromdate.Date;
        var queryable = await _context.Set<MoodDay>().AsNoTracking().SingleOrDefaultAsync(p=>p.Id==id && p.Date.Date==dateonly);
        return queryable;
    }

    public async void Update(MoodDay obj)
    {
        _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
