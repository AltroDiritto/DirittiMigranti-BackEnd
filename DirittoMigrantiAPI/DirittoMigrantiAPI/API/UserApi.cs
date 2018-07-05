using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    public class UserControllerAPI : UserController, IConsultantAPI, IOperatorAPI
    {
        private readonly UserContext context;

        public UserControllerAPI(UserContext context) : base(context.Users)
        {
            this.context = context;
        }

        public TokenRequest Login(User credentials)
        {
            //TODO
            return null;
        }

        private IActionResult GetUser(long userId)
        {
            //TODO
            return null;
        }

        #region Operator
        [AllowAnonymous]
        [HttpPost]
        public IActionResult NewOperator([FromBody] Operator @operator)
        {
            var result = NewOperator(@operator);

            if (result == null) return BadRequest();

            return Ok();
        }

        IActionResult IOperatorAPI.GetOperator(long userId)
        {
            //TODO
            throw new NotImplementedException();
        }

        public IActionResult GetAllOperators()
        {
            //TODO
            throw new NotImplementedException();
        }

        public IActionResult SetOperatorState(long userId, bool newState)
        {
            //TODO
            throw new NotImplementedException();
        }
        #endregion

        #region Consultant
        IActionResult IConsultantAPI.GetConsultant(long userId)
        {
            //TODO
            throw new NotImplementedException();
        }
        #endregion

    }
}
