using System;
using System.Security.Claims;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    //todo cambiare nome
    [Route("api/login")]
    public class LoginApi : Controller
    {
        //TODO username e pw divisi da altre info

        private readonly UserContext _context;

        public LoginApi(UserContext context)
        {
            _context = context;
        }




        // POST api/login
        [HttpPost]
        public IActionResult TryToLogin([FromBody] TokenRequest tokenRequest)
        {
            //TokenRequest è una nostra classe contenente le proprietà Username e Password
            //Avvisiamo il client se non ha fornito tali valori
            if (!ModelState.IsValid)
            {
                View(tokenRequest);
                return BadRequest();
            }

            //Lo avvisiamo anche se non ha fornito credenziali valide
            if (!CheckCredentials(tokenRequest.Username, tokenRequest.Password))
                return Unauthorized();

            //Ok, l'utente ha fornito credenziali valide, creiamogli una ClaimsIdentity
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //Aggiungiamo uno o più claim relativi all'utente loggato
            identity.AddClaim(new Claim(ClaimTypes.Name, tokenRequest.Username));
            //Incapsuliamo l'identità in una ClaimsPrincipal l'associamo alla richiesta corrente
            HttpContext.User = new ClaimsPrincipal(identity);

            //Non è necessario creare il token qui, lo possiamo creare da un middleware (perchè?)
            return NoContent();
        }

        private bool CheckCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
