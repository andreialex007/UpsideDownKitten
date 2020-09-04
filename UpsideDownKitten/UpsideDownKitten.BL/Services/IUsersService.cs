using UpsideDownKitten.BL.Models;

namespace UpsideDownKitten.BL.Services
{
    public interface IUsersService
    {
        void Create(string email, string password);
        bool HasUser(string email, string password);
        UserDto Get(string email);
        UserDto Get(string email, string password);
    }
}