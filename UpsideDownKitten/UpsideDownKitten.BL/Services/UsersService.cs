using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UpsideDownKitten.BL.Common;
using UpsideDownKitten.BL.Models;
using UpsideDownKitten.BL.Services.Interfaces;
using UpsideDownKitten.DL;

namespace UpsideDownKitten.BL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void Create(string email, string password)
        {
            ValidateEmailPassword(email, password);
            var user = _usersRepository.Get(email);
            if (user != null)
            {
                throw new WebApiException("User with same email already exists");
            }
            _usersRepository.Create(email,password);
        }

        public bool HasUser(string email, string password)
        {
             return _usersRepository.Get(email, password) != null;
        }

        public UserDto Get(string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new WebApiException("You have to provide valid email");
            }
            var user = _usersRepository.Get(email);
            if (user == null)
            {
                throw new WebApiException("Unable to find user with given email");
            }
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public UserDto Get(string email, string password)
        {
            ValidateEmailPassword(email, password);
            var user = _usersRepository.Get(email, password);
            if (user == null)
            {
                throw new WebApiException("Wrong username or password.");
            }
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public List<UserDto> All()
        {
            return _usersRepository.All()
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email
                })
                .ToList();
        }

        private static void ValidateEmailPassword(string email, string password)
        {
            if (!new EmailAddressAttribute().IsValid(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new WebApiException("You have to provide valid email and not empty password");
            }
        }
    }
}
