using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace DirittoMigrantiAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserContext _context;

        public LoginController(UserContext context)
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
            User user = null;
            if (_context.Users.Any((u) => u.Username == username && u.Password == password))
                user = _context.Users.Where((u) => u.Username == username && u.Password == password).First();

            return user != null;
        }
    }

}
