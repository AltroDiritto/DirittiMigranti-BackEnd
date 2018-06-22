using System;
using System.Text;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DirittoMigrantiAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region Contexts
            //List of Consultant and Operators
            services.AddDbContext<UserContext>(options =>
                                               options.UseInMemoryDatabase("UserList"));

            //List of Conversations with Messages
            services.AddDbContext<MessageExchangesContext>(options =>
                                                           options.UseInMemoryDatabase("ConversationsList"));

            //List of News and Practices
            services.AddDbContext<ContentContext>(options =>
                                                  options.UseInMemoryDatabase("ContentsList"));
            #endregion

            #region Middleware
            //Configurare il middleware di autenticazione di ASP.NET Core per supportare i token JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    //Importante: indicare lo stesso Issuer, Audience e chiave segreta
                    //usati anche nel JwtTokenMiddleware
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Program.key)
                  ),
                    //Tolleranza sulla data di scadenza del token
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseMiddleware<JwtTokenMiddleware>();
            app.UseAuthentication();

#if DEBUG
            var userContext = serviceProvider.GetRequiredService<UserContext>();
            AddTestData(userContext);
#endif

            app.UseMvc();
        }

        private static void AddTestData(UserContext context)
        {
            Operator @operator = new Operator
            {
                Username = "utente1",
                Password = "utente1",
                IsActive = true
            };
            context.Users.Add(@operator);

            Consultant consultant = new Consultant
            {
                Username = "utente2",
                Password = "utente2"
            };
            context.Users.Add(consultant);

            context.SaveChanges();
        }
    }
}
