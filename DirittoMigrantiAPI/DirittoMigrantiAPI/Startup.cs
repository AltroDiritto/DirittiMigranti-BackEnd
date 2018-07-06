using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using DirittoMigrantiAPI.Models.Users;
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
        static List<User> users;

        public void ConfigureServices(IServiceCollection services)
        {
            #region Contexts
            //TODO: la connectionstring e ogni altro parametro di configurazione
            //andrebbero messi nel file appsettings.json
            //Vedi: https://docs.microsoft.com/en-US/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration

            //List of Consultant and Operators
            services.AddDbContext<UserContext>(options =>
                                               options.UseSqlite("Data Source=diritto-migranti-user.db"));

            //List of Conversations with Messages
            services.AddDbContext<MessageExchangesContext>(options =>
                                                           options.UseSqlite(@"Data Source=diritto-migranti-msg.db"));

            //List of News and Practices
            services.AddDbContext<ContentContext>(options =>
                                                  options.UseSqlite(@"Data Source=diritto-migranti-content.db"));
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

            #region DEBUG
            var userContext = serviceProvider.GetRequiredService<UserContext>();
            userContext.Database.EnsureCreated();
            AddUserTestData(userContext);

            var contentContext = serviceProvider.GetRequiredService<ContentContext>();
            contentContext.Database.EnsureCreated();
            // AddContentTestData(contentContext);

            var conversationContext = serviceProvider.GetRequiredService<MessageExchangesContext>();
            conversationContext.Database.EnsureCreated();
            // AddConversationTestData(conversationContext);

            var users = userContext.Users.ToList();
            var contents = contentContext.Contents.ToList();
            #endregion

            if (env.IsDevelopment())
            {
                app.AddEfDiagrams<UserContext>();
            }

            app.UseMvc();
        }


        private static void AddUserTestData(UserContext context)
        {
            users = new List<User>();

            //creo utente
            var operator1 = new Operator
            {
                Email = "utente18@example.com",
                IsActive = true
            };
            var s = context.Users.Add(operator1);

            //creo credenziali
            var auth1 = new UserAuth();
            auth1.Username = "a";
            auth1.Password = "a";
            auth1.UserId = s.Entity.Id;
            context.UsersAuth.Add(auth1);


            //creo utente
            Consultant consultant = new Consultant
            {
                Email = "utente882@example.com"
            };
            s=context.Users.Add(consultant);

            //creo credenziali
            var auth2 = new UserAuth();
            auth2.Username = "b";
            auth2.Password = "b";
            auth2.UserId = s.Entity.Id;
            context.UsersAuth.Add(auth2);

            context.SaveChanges();

            users.Add(operator1);
            users.Add(consultant);
        }

        private static void AddConversationTestData(MessageExchangesContext context)
        {
            Message firstMessage = new Message(users[0], "Testo di prova 1 messaggio");
            MessageExchange conv = new MessageExchange(firstMessage);

            Message secondMessage = new Message(users[1], "Testo secondo");

            conv.AddMessage(secondMessage);

            context.MessageExchanges.Add(conv);

            context.SaveChanges();
        }

        //Questo vale sia per le news che per le pratiche @Gianluca (Entrambe sono content)
        private static void AddContentTestData(ContentContext context)
        {

            News news1 = new News((Consultant)users[1], "Titolo news 1", "Lorem ipsum.....");
            News news2 = new News((Consultant)users[1], "Titolo news 2", "Lorem ipsum....", "http://attachedURL.com/");
            news2.Publish();

            context.Contents.Add(news1);
            context.Contents.Add(news2);

            Practice practice1 = new Practice((Consultant)users[1], "Titolo practica 1", "Lorem ipsum...", true);
            Practice practice2 = new Practice((Consultant)users[1], "Titolo practica 2", "Lorem ipsum...", false);

            context.Contents.Add(practice1);
            context.Contents.Add(practice1);

            context.SaveChanges();
        }
    }
}