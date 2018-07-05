using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    public class UserApi :  UserController
    {
        private readonly UserContext context;

        public UserApi(UserContext context) : base(context.Users)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult NewOperator([FromBody] Operator @operator)
        {
            var result = NewOperator(@operator);

            if (result == null) return BadRequest();

            return Ok();
        }
    }
}
