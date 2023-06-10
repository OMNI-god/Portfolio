using Portfolio.Models;

namespace Portfolio.Data.Iservices
{
    public interface IUserServices
    {
        void register(User user);
        void login(User user);
        void logout();
    }
}
