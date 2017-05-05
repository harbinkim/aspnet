using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
        User GetUser(int userId);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserRepository:IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
            && t.UserPassword == password);
            
            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users.Add(new User { UserEmail = email, UserPassword = password });
            DatabaseAccessor.Instance.SaveChanges();
            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public User GetUser(int userId)
        {
            var user = DatabaseAccessor.Instance.Users.First(u => u.UserId == userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}