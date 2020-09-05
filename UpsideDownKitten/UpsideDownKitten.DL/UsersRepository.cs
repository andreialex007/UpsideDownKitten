using System.Collections.Generic;
using System.Linq;

namespace UpsideDownKitten.DL
{
    public class UsersRepository : IUsersRepository
    {
        private static readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Email = "admin@admin.com",
                Password = "123"
            }
        };

        public void Create(string email, string password)
        {
            var id = (_users.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0);
            _users.Add(new User
            {
                Id = ++id,
                Email = email,
                Password = password
            });
        }

        public User Get(string email)
        {
            return _users.SingleOrDefault(x => x.Email == email);
        }

        public User Get(string email, string password)
        {
            return _users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

        public List<User> All()
        {
            return _users;
        }
    }
}
