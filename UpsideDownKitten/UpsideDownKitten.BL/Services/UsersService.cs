﻿using System.ComponentModel.DataAnnotations;
using UpsideDownKitten.BL.Models;
using UpsideDownKitten.BL.Utils;
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
            //todo validation
            _usersRepository.Create(email,password);
        }

        public bool HasUser(string email, string password)
        {
             return _usersRepository.Get(email, password) != null;
        }

        public UserDto Get(string email)
        {
            if(!new EmailAddressAttribute().IsValid(email))
                throw new WebApiException("You have to provide valid email");

            var user = _usersRepository.Get(email);
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public UserDto Get(string email, string password)
        {
            var user = _usersRepository.Get(email, password);
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
            };
        }

    }
}
