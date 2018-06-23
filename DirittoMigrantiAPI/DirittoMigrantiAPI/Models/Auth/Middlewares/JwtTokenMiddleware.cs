using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace DirittoMigrantiAPI.Models
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate next;
        public JwtTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var identity = context.User.Identity as ClaimsIdentity;

                //Se la richiesta era autenticata, allora creiamo un nuovo token JWT
                if (identity.IsAuthenticated)
                {
                    //Il client potrà usare questo nuovo token nella sua prossima richiesta
                    var token = CreateTokenForIdentity(identity);
                    //Usiamo l'intestazione X-Token, ma non è obbligatorio che si chiami così
                    context.Response.Headers.Add("X-Token", token);
                }
                return Task.CompletedTask;
            });
            await next.Invoke(context);
        }

        //In questo metodo creiamo il token a partire dai claim della ClaimsIdentity
        private StringValues CreateTokenForIdentity(ClaimsIdentity identity)
        {
            //Chiave simmetrica per cifrare
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Creo il token
            var token = new JwtSecurityToken(
              issuer: "Issuer",
              audience: "Audience",
              claims: identity.Claims,
              expires: DateTime.Now.AddMinutes(30), //Dura 30 min
                signingCredentials: credentials

            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var serializedToken = tokenHandler.WriteToken(token);
            return serializedToken;
        }
    }
    /*
     * Audience represents the intended recipient of the incoming token or the resource that the token grants access to. If the value specified in this parameter doesn’t match the aud parameter in the token, the token will be rejected because it was meant to be used for accessing a different resource. Note that different security token providers have different behaviors regarding what is used as the ‘aud’ claim (some use the URI of a resource a user wants to access, others use scope names). Be sure to use an audience that makes sense given the tokens you plan to accept.
     * */
}
