using System;
using System.Linq;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly DbSet<User> users;
        public UserController(DbSet<User> users)
        {
            this.users = users;
        }

        private User GetUser(long id)
        {
            return users.Find(id);
        }

        #region Consultant
        protected Consultant GetConsultant(long id)
        {
            return (Consultant)GetUser(id);
        }
        #endregion


        #region Operator
        protected Operator GetOperator(long id)
        {
            return (Operator)GetUser(id);
        }
        protected Operator NewOperator(Operator @operator)
        {
            if (users.Contains(@operator)) return null;

            users.Add(@operator);
            return @operator;
        }
        protected bool ChangeState(long operatorId, bool newState)
        {
            Operator _operator = GetOperator(operatorId);
            return _operator.ChangeState(newState);
        }
        #endregion

        private bool CheckCredentials(string username, string password)
        {
            User user = null;
            if (users.Any((u) => u.Username == username && u.Password == password))
                user = users.Where((u) => u.Username == username && u.Password == password).First();

            return user != null;
        }
    }
}
