using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpsideDownKitten.DL
{
    public class UsersRepository
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

        public UsersRepository()
        {
            
        }

        public void CreateUser(string email, string password)
        {
            var id = (_users.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0);
            _users.Add(new User
            {
                Id = ++id,
                Email = email,
                Password = password
            });
        }

        public bool Login(string email, string password)
        {
            return _users.Any(x=>x.Email == email && x.Password == password);
        }

        public User GetUserInfo(string email)
        {
            return _users.SingleOrDefault(x => x.Email == email);
        }
        public User GetUserInfo(string email, string password)
        {
            return _users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

    }
}
