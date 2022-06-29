using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Services;

public class FlatService:IFlatService
{
    private readonly IRepository<Flat> _repository;

    public FlatService(IRepository<Flat> repository)
    {
        _repository = repository;
    }
}

public interface IFlatService
{
}