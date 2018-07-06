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
        static public List<News> news;  //Alternativa: fare una lista unica di Content, così mi pare più ordinato
        static public List<Practice> practices;   //news e practice avrei potuto dichiararle anche dentro alle funzioni
        


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
            AddContentTestData(contentContext);
            
            var conversationContext = serviceProvider.GetRequiredService<MessageExchangesContext>();
            AddConversationTestData(conversationContext);
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
            news = new List<News>();
            practices = new List<Practice>();

            News news1 = new News((Consultant)users[1], "Titolo news 1", "Lorem ipsum.....");
            news.Add(news1);
            News news2 = new News((Consultant)users[1], "Titolo news 2", "Lorem ipsum....", "http://attachedURL.com/");
            news.Add(news2);
            news2.Publish();   //Perchè questo? @Gianluca
            
            context.Add(news);

            context.SaveChanges(); //@Leo non so se basti chiamarlo una volta in fondo            

            Practice practice1 = new Practice((Consultant)users[1], "Titolo practica 1", "Lorem ipsum...", true);
            practices.Add(practice1);
            Practice practice2 = new Practice((Consultant)users[1], "Titolo practica 2", "Lorem ipsum...", false);
            practices.Add(practice2);

            context.Add(practices);         

            context.SaveChanges();   //Forse basta chiamare questo
        }
    }
}
