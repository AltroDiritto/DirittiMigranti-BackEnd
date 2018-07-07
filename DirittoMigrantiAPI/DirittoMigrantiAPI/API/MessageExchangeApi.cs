using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    [Route("api/conv")]
    public class MessageExchangeApi : MessageExchangesController
    {
        private readonly MessageExchangesContext _context;

        public MessageExchangeApi(MessageExchangesContext context, UserContext userContext) : base(context.MessageExchanges)
        {
            this._context = context;
        }


        [Authorize(Roles = "Operator")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("newC", Name = "NewConversation")]
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
            return CreatedAtRoute("GetME", new { id = messageExchange.Id }, messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //NOTA: il Name serve per la chiamata da qua dentro.        
        [HttpGet("getME/{id}", Name = "GetMessageExchange")]
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

        [Authorize(Roles = "Operator, Consultant")]
        [HttpGet("getMELastU", Name = "GetAllMessageExchangesOrderByLastUpdate")]
        public IActionResult GetListOrderedByLastUpdate()
        {
            var messageExchange = base.GetAllMessageExchangesOrderByLastUpdate();
            if (messageExchange == null)
            {
                return NotFound();
            }
            return Ok(messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [HttpGet("getMECr", Name = "GetAllMessageExchangeByCreationDate")]
        public IActionResult GetListOrderedByCreationDate()
        {
            var messageExchange = base.GetAllMessageExchangeByCreationDate();
            if (messageExchange == null)
            {
                return NotFound();
            }
            return Ok(messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [HttpPost("addM", Name = "addMessage")]
        public IActionResult AddMessage(long conversationId, [FromBody] Message message)
        {
            var flag = base.AddMessageToConversation(conversationId, message);
            if (flag == false)
            {
                return BadRequest();
            }
            return Ok(flag);
        }

        [Authorize(Roles = "Consultant")]
        [HttpGet("getN/{conversationId}", Name = "getNotes")]
        public IActionResult GetNotesAPI(long conversationId)
        {
            String notes = null;
            notes = base.GetMessageExchange(conversationId).Notes;
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        [Authorize(Roles = "Consultant")]
        [HttpPost("editN", Name = "editNotes")]
        public IActionResult EditNotes(long conversationId, string text)
        {
            string notes = null;
            notes = base.EditNotesInConversation(conversationId, text);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        //[Authorize(Roles = "Consultant")]
        //[HttpGet]
        //public IActionResult GetStarred()
        //{
        //    //TODO cambiare il contesto da cui viene preso l'elenco conversazioni una volta che verrà inserito nel costruttore
        //    var starredConversation = base.GetstarredConversations();
        //    if (starredConversation == null)
        //        return NotFound();
        //    return Ok(starredConversation);
        //}

        //[Authorize(Roles = "Consultant")]
        //[HttpPost]
        //public IActionResult SetStarred(long conversationId)
        //{
        //    //  TODO cambiare contesto da cui viene preso il metodo StarConversation
        //    //cosa ritorna???? 
        //    if (userContext.StarConversation(base.GetMessageExchange(conversationId)))
        //        return Ok(true);
        //    return NotFound();
        //    throw new NotImplementedException();
        //}
    }
}
