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
        private readonly MessageExchangesContext _context;

        public MessageExchangeApi(MessageExchangesContext context, UserContext userContext) : base(context.MessageExchanges)
        {
            this._context = context;
        }


        [Authorize(Roles = "Manager,Administrator")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        //TODO controllo se è un'operatore
        //Se è un operatore non dovrebbe essere già gestito da Authorize
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
            _context.SaveChanges();//TODO controlla eccezioni

            // Invio come risposta le info dell'utente appena creato
            return CreatedAtRoute("GetMessageExchange", new { id = messageExchange.Id }, messageExchange);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //NOTA: il Name serve per la chiamata da qua dentro.        
        [HttpGet("getMessageExchange/{id}", Name = "GetMessageExchange")]
        public IActionResult GetById(long id)
        {
            //TODO ottenere lo user 
            Consultant user = null;

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

        public IActionResult GetListOrderedByLastUpdate()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetListOrderedByCreationDate()
        {
            throw new NotImplementedException();
        }

        public IActionResult AddMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetNotes(long conversationId)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditNotes(long conversationId, string text)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetStarred()
        {
            throw new NotImplementedException();
        }

        public IActionResult SetStarred(long conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
