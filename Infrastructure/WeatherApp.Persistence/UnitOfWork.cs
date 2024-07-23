using WeatherApp.Persistence.Context;
using WeatherApp.Services.Contract.Base.Repositories;

namespace WeatherApp.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Complete()
    {
        await _dataContext.SaveChangesAsync();
    }
}