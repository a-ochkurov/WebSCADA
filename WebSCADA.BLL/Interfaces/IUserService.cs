using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Interfaces
{
    public interface IUserService
    {
        UserDomain Get(string login, string password);

        void Add(UserDomain userDomain);
    }
}
