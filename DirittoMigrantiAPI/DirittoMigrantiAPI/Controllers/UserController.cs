using System;
using System.Linq;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.Controllers
{
    public class UserController : Controller
    {
        protected readonly UserContext _context;
        public UserController()
        {
        }

        public abstract User GetUser();

        public abstract User Register();

        private bool CheckCredentials(string username, string password)
        {
            User user = null;
            if (_context.Users.Any((u) => u.Username == username && u.Password == password))
                user = _context.Users.Where((u) => u.Username == username && u.Password == password).First();

            return user != null;
        }
    }
}
