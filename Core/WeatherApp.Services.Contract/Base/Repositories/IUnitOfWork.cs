namespace WeatherApp.Services.Contract.Base.Repositories;

public interface IUnitOfWork
{
    Task Complete();
}