using System;
using System.Linq;
using System.Security.Claims;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    [Route("api/user")]
    public class UserControllerAPI : UserController, IConsultantAPI, IOperatorAPI
    {
        private readonly UserContext context;
        public UserControllerAPI(UserContext context) : base(context.Users,context.UsersAuth)
        {
            this.context = context;
        }

        // POST api/login
        [Route("api/login")]
        [HttpPost]
        public IActionResult TryToLogin([FromBody] UserAuth userAuth)
        {
            //TokenRequest è una nostra classe contenente le proprietà Username e Password
            //Avvisiamo il client se non ha fornito tali valori
            if (!ModelState.IsValid)
            {
                View(userAuth);
                return BadRequest();
            }

            //Lo avvisiamo anche se non ha fornito credenziali valide
            if (!CheckCredentials(userAuth))
                return Unauthorized();


            //Ok, l'utente ha fornito credenziali valide, creiamogli una ClaimsIdentity
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            //Aggiungiamo uno o più claim relativi all'utente loggato
            long userId = GetUserId(userAuth);
            User user = context.Users.Find(userId);

            identity.AddClaim(new Claim(ClaimTypes.Role, user is Operator ? "Operator" : "Consultant"));
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userId.ToString()));

            //Incapsuliamo l'identità in una ClaimsPrincipal l'associamo alla richiesta corrente
            HttpContext.User = new ClaimsPrincipal(identity);

            //Non è necessario creare il token qui, lo possiamo creare da un middleware (perchè?)
            return NoContent();
        }

        
        private IActionResult GetUser(long userId)
        {
            //TODO
            return null;
        }

        #region Operator
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult NewOperator([FromBody] Operator @operator)
        {
            if (!ModelState.IsValid)
            {
                View(@operator);
                return BadRequest();
            }

            return Ok();
        }

        IActionResult IOperatorAPI.GetOperator(long userId)
        {
            //string sn = (User as ClaimsPrincipal)?.FindFirst(ClaimTypes.SerialNumber)?.Value;

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


       


        //Non è necessario creare il token qui, lo possiamo creare da un middleware (perchè?)

    }
}
