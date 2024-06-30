using DAL.Interfaces;
using DAL.Models;
using DAL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private static IList<User> users = new List<User>()
        {
            new User() {Username = "admin", Password = "admin"},
            new User() {Username = "user", Password = "user" }
        };

        public bool Authenticate(LoginRequest request)
        {
            return users.Any(u => u.Username.ToLower().Equals(request.Username.ToLower()) && u.Password.Equals(request.Password));
        }
    }
}
