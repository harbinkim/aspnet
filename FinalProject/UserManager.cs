using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject
{
    public interface IUserManager
    {
        UserManagerModel LogIn(string email, string password);
        UserManagerModel Register(string email, string password);
        User GetUser(int userId);
    }
    public class UserManagerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserManagerModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserManagerModel { Id = user.Id, Name = user.Name };
        }

        public UserManagerModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserManagerModel { Id = user.Id, Name = user.Name };
        }

        public User GetUser(int userId)
        {
            return userRepository.GetUser(userId);

        }
    }
}