using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    [Route("api/[controller]")]
    public class MessageExchangeApi : MessageExchangesController
    {
        private readonly MessageExchangesContext context;

        public MessageExchangeApi(MessageExchangesContext context) : base(context.MessageExchanges)
        {
            this.context = context;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //NOTA: il Name serve per la chiamata da qua dentro.        
        [HttpGet("getMessageExchange/{id}", Name = "GetMessageExchange")]
        public IActionResult GetById(long id)
        {
            //TODO ottenere lo user 
            Consultant user=null;

            var messageExchange = GetMessageExchange(id);
            if (messageExchange == null)
            {
                return NotFound();
            }

            if (user is Consultant || messageExchange.IsThisUserTheOwner(user))
            {
                return Ok(messageExchange);
            }
            else return NotFound();
        }

        [Authorize(Roles = "Manager,Administrator")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        //TODO controllo se è un'operatore
        public IActionResult Create([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var messageExchange = NewConversation(message);

            if (messageExchange == null)
                return BadRequest();

            // Salvo
            context.SaveChanges();

            // Invio come risposta le info dell'utente appena creato
            return CreatedAtRoute("GetMessageExchange", new { id = messageExchange.Id }, messageExchange);
        }
    }
}
