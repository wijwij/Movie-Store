namespace MovieStore.Core.ServiceInterfaces
{
    public interface ICryptoService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
    }
}