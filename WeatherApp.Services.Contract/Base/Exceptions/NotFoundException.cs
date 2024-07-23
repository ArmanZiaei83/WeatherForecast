namespace WeatherApp.Services.Contract.Base.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}