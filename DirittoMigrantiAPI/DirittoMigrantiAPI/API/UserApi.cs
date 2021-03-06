﻿using System;
using System.Linq;
using System.Security.Claims;
using DirittoMigrantiAPI.Contexts;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    [Route("api/user")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserControllerAPI : UserController, IConsultantAPI, IOperatorAPI
    {
        private readonly MyAppContext context;
        public UserControllerAPI(MyAppContext context) : base(context.Users, context.UsersAuth)
        {
            this.context = context;
        }

        [HttpPost("login", Name = "Login")]
        public IActionResult TryToLogin([FromBody] UserAuth userAuth)
        {
            // TokenRequest è una nostra classe contenente le proprietà Username e Password
            //Avvisiamo il client se non ha fornito tali valori
            if (!ModelState.IsValid) return BadRequest();

            // check username and get userid
            var userId = GetUserId(userAuth.Username);
            if (!userId.HasValue) Unauthorized();

            // check credentials
            if (!CheckCredentials(userAuth)) return Unauthorized();

            // get user
            User user = GetUser(userId.Value);
            if (user == null) Unauthorized();

            //Ok, l'utente ha fornito credenziali valide, creiamogli una ClaimsIdentity
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);


            //Aggiungiamo uno o più claim relativi all'utente loggato
            identity.AddClaim(new Claim(ClaimTypes.Role, user is Operator ? "Operator" : "Consultant"));
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userId.ToString()));

            //Incapsuliamo l'identità in una ClaimsPrincipal l'associamo alla richiesta corrente
            HttpContext.User = new ClaimsPrincipal(identity);

            //Non è necessario creare il token qui, lo possiamo creare da un middleware (perchè?)
            return NoContent();
        }


        #region Operator
        //[AllowAnonymous]
        [HttpPost("newOp", Name = "NewOperator")]
        public IActionResult NewOperatorAPI([FromBody] UserAuth auth, [FromBody] Operator op)
        {
            if (!ModelState.IsValid)
            {
                View(op);
                return BadRequest();
            }

            Operator checkOperator = NewOperator(op);

            if (checkOperator == null)
                return BadRequest();

            context.SaveChanges();

            return Ok(checkOperator);
        }

        [Authorize(Roles = "Consultant")]
        [HttpGet("getOp/{userId}", Name = "GetOperator")]
        public IActionResult GetOperatorAPI(long userId)
        {
            //string sn = (User as ClaimsPrincipal)?.FindFirst(ClaimTypes.SerialNumber)?.Value;
            var op = GetOperator(userId);
            if (op == null)
                return NotFound();
            return Ok(op);
        }

        [Authorize(Roles = "Consultant")]
        [HttpGet("getAllOp", Name = "getAllOperator")]
        public IActionResult GetAllOperatorsAPI()
        {
            var allOperators = GetAllOperator();
            if (allOperators == null)
                return NotFound();
            return Ok(allOperators);
        }

         [Authorize(Roles = "Consultant")]
        [HttpPost("setOpState/{userId}/{newState}", Name = "SetOperatorState")]
        public IActionResult SetOperatorStateAPI( long userId, bool newState)
        {
            var ris = ChangeState(userId, newState);
            context.SaveChanges();

            return Ok(ris);
        }
        #endregion

        #region Consultant
        [HttpGet("getCons/{userId}", Name = "getConsultant")]
        public IActionResult GetConsultantAPI(long userId)
        {
            var consultant = GetConsultant(userId);
            if (consultant == null)
                return NotFound();
            return Ok(consultant);
        }

        [HttpGet("getMyUser", Name = "getMyUser")]
        public IActionResult GetMyUser()
        {
            var userId = (User as ClaimsPrincipal)?.FindFirstValue(ClaimTypes.SerialNumber);
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            var user = GetUser(long.Parse(userId));
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        #endregion
        //Non è necessario creare il token qui, lo possiamo creare da un middleware (perchè?)
    }
}
