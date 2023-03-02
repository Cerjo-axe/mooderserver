using AutoMapper;
using Domain.Entity;
using DTO;

namespace Infra.Cross;

public class MooderProfile : Profile
{
    public MooderProfile()
    {
        CreateMap<MoodDay,MoodDayDTO>().ReverseMap();
    }
}
