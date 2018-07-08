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
        private readonly UserContext _context;
        public UserControllerAPI(UserContext context) : base(context.Users, context.UsersAuth)
        {
            this._context = context;
        }

        [HttpPost("login", Name = "Login")]
        public IActionResult TryToLogin([FromBody] UserAuth userAuth)
        {
            //TokenRequest è una nostra classe contenente le proprietà Username e Password
            //Avvisiamo il client se non ha fornito tali valori
            if (!ModelState.IsValid)
            {
                //View(userAuth);
                return BadRequest();
            }

            //Lo avvisiamo anche se non ha fornito credenziali valide
            if (!CheckCredentials(userAuth))
                return Unauthorized();


            long userId = GetUserId(userAuth);
            User user = GetUser(userId);

            //TODO check user!=null

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
        public IActionResult NewOperatorAPI([FromForm] UserAuth auth,[FromForm] Operator op)
        {
            if (!ModelState.IsValid)
            {
                View(op);
                return BadRequest();
            }

            Operator checkOperator = NewOperator(op);

            if (checkOperator == null)
                return BadRequest();

            _context.SaveChanges();

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
        [HttpPost("setOpState", Name = "SetOperatorState")]
        public IActionResult SetOperatorStateAPI([FromForm] long userId, [FromForm] bool newState)
        {
            return Ok(ChangeState(userId, newState));
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
