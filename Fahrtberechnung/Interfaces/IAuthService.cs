namespace Fahrtberechnung.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string username, string password);
    }
}
