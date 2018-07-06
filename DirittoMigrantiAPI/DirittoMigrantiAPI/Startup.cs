using System;
using System.Collections.Generic;
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
        static public List<User> users;


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

            #region DEBUG
            var userContext = serviceProvider.GetRequiredService<UserContext>();
            AddUserTestData(userContext);

            var contentContext = serviceProvider.GetRequiredService<ContentContext>();


            var conversationContext = serviceProvider.GetRequiredService<MessageExchangesContext>();

            AddConversationTestData(conversationContext, userContext);
            #endregion


            app.UseMvc();
        }


        private static void AddUserTestData(UserContext context)
        {
            users = new List<User>();

            var operator1 = new Operator
            {
                Username = "utente1",
                Password = "utente1",
                IsActive = true
            };
            context.Users.Add(operator1);

            Consultant consultant = new Consultant
            {
                Username = "utente2",
                Password = "utente2",
            };
            context.Users.Add(consultant);

            context.SaveChanges();

            users.Add(operator1);
            users.Add(consultant);
        }

        private static void AddConversationTestData(MessageExchangesContext context, UserContext userContext)
        {
            Message firstMessage = new Message(users[0], "Testo di prova 1 messaggio");
            MessageExchange conv = new MessageExchange(firstMessage);

            Message secondMessage = new Message(users[1], "Testo secondo");
            conv.AddMessage(secondMessage);

            context.Add(conv);

            context.SaveChanges();
        }

        private static void AddNewsTestData(ContentContext context){
            var news1 = new News((Consultant)users[1], "Titolo news 1", "Lorem ipsum.....");
            var news2 = new News((Consultant)users[1], "Titolo news 2", "Lorem ipsum....", "http://attachedURL.com/");
            news2.Publish();

            context.Add(news1); //context.News.Add(news1)????
            context.Add(news2);

            context.SaveChanges();
        }

        private static void AddPracticeTestData(ContentContext context){
            var practice1 = new Practice((Consultant)users[1], "Titolo practica 1", "Lorem ipsum...", true);
            var practice2 = new Practice((Consultant)users[1], "Titolo practica 2", "Lorem ipsum...", false);

            context.Add(practice1);
            context.Add(practice2);

            context.SaveChanges();

        }
    }
}
