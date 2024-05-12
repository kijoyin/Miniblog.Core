namespace Miniblog.Brains.Services
{
    public interface IUserServices
    {
        bool ValidateUser(string username, string password);
    }
}
