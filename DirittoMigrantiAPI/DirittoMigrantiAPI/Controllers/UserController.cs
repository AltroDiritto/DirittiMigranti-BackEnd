using System;
using System.Linq;
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
            return usersAuth.Any((u) => u.CheckCredentials(userAuth));
        }

        protected long GetUserId(UserAuth userAuth)
        {
            return usersAuth.Where((u) => u.CheckCredentials(userAuth)).ToList()[0].UserId;
        }

        private User GetUser(long id)
        {
            return users.Find(id);
        }

        #region Consultant
        public Consultant GetConsultant(long id)
        {
            return (Consultant)GetUser(id);
        }
        #endregion


        #region Operator
        public Operator GetOperator(long id)
        {
            return (Operator)GetUser(id);
        }

        public Operator NewOperator(Operator @operator)
        {
            if (users.Contains(@operator)) return null;

            users.Add(@operator);
            return @operator;
        }

        public bool ChangeState(long operatorId, bool newState)
        {
            Operator _operator = GetOperator(operatorId);
            return _operator.ChangeState(newState);
        }

        public DbSet<User> GetAllOperator()
        {
            //TODO
            return null;
        }
        #endregion
    }
}
