
using FluentValidation;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using DTO;
using Service.Validators;

namespace Service.Services;

public class MoodDayService : IMoodDayService
{

    private readonly IMoodDayRepository _repository;
    private readonly IMapper _mapper;

    public MoodDayService(IMoodDayRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MoodDayDTO> Add(MoodDayDTO obj)
    {
        MoodDay entity = _mapper.Map<MoodDay>(obj);
        Validate(entity, Activator.CreateInstance<MoodDayValidate>());
        await _repository.Add(entity);
        MoodDayDTO moodDayDTO = _mapper.Map<MoodDayDTO>(entity);
        return moodDayDTO;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public MoodDayDTO Update(MoodDayDTO obj)
    {
        MoodDay entity = _mapper.Map<MoodDay>(obj);
        Validate(entity, Activator.CreateInstance<MoodDayValidate>());
        _repository.Update(entity);
        MoodDayDTO moodDayDTO = _mapper.Map<MoodDayDTO>(entity);
        return moodDayDTO;
    }

    public MoodDayDTO Get(int id)
    {
        var entity = _repository.Get(id);
        MoodDayDTO output = _mapper.Map<MoodDayDTO>(entity);
        return output;
    }

    public async Task<IEnumerable<MoodDayDTO>> GetAll()
    {
        var entities = await _repository.GetAll();
        var outputs = entities.Select(s=> _mapper.Map<MoodDayDTO>(s));
        return outputs;
    }

    private void Validate(MoodDay entity, AbstractValidator<MoodDay> validator)
    {
        if(entity ==null){
            throw new Exception("Registro não detectado");
        }

        validator.ValidateAndThrow(entity);
    }
}
