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

    public async void Delete(int id)
    {
        _context.Set<MoodDay>().Remove(await Get(id));
        await _context.SaveChangesAsync();

    }

    public async Task<MoodDay> Get(int id)
    {
        return await _context.Set<MoodDay>().FindAsync(id);
    }

    public async Task<IEnumerable<MoodDay>> GetAll()
    {
        try
        {
            return await _context.MoodDays.ToListAsync();
        }
        catch (Exception ex)
        {         
            throw ex;
        }
        
    }

    public async void Update(MoodDay obj)
    {
        _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
