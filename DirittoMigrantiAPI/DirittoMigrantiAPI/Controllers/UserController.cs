using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class UserController : Controller, IConsultantController, IOperatorController
    {
        private readonly DbSet<User> users;
        private readonly DbSet<UserAuth> usersAuth;

        public UserController(DbSet<User> users, DbSet<UserAuth> usersAuth)
        {
            this.users = users;
            this.usersAuth = usersAuth;
        }

        protected bool CheckCredentials(UserAuth userAuth)
        {
            var result = usersAuth.Find(userAuth.Username);
            if (result == null) return false;
            return result.CheckPassword(userAuth.Password);
        }

        protected long? GetUserId(String userName)
        {
            var result = usersAuth.Find(userName);
            if (result == null) return null;
            return result.UserId;
        }

        protected User GetUser(long id)
        {
            return users.Find(id);
            //TODO utilizzare il find
            //return users.Where((user) => user.Id == id).Single();
        }

        protected T GetUser<T>(long id)
        {
            User result = GetUser(id);
            if (result == null || !(result is T)) return default(T);
            return (T)(object)result;
        }

        #region Consultant
        public Consultant GetConsultant(long id)
        {            
            return GetUser<Consultant>(id);
        }       

        #endregion


        #region Operator
        public Operator GetOperator(long id)
        {
            var result = GetUser(id);
            if (result == null || !(result is Operator)) return null;
            return (Operator)result;
        }

        public Operator NewOperator(Operator op)
        {
            if (users.Contains(op)) return null;

            users.Add(op);
            return op;
        }

        public bool ChangeState(long operatorId, bool newState)
        {
            Operator _operator = GetOperator(operatorId);
            if (_operator == null) return false;
            return _operator.ChangeState(newState);
        }

        public List<Operator> GetAllOperator()
        {
            return users.Where((user) => user is Operator).OfType<Operator>().ToList();
        }
        #endregion
    }

}
