using System.Collections.Generic;

namespace UpsideDownKitten.DL
{
    public interface IUsersRepository
    {
        void Create(string email, string password);
        User Get(string email);
        User Get(string email, string password);
        List<User> All();
    }
}