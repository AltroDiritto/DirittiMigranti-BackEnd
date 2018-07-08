using System;
using System.Linq;
using System.Security.Claims;
using DirittoMigrantiAPI.Contexts;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    [Route("api/conv")]
    public class MessageExchangeApi : MessageExchangesController
    {
        private readonly MyAppContext context;

        public MessageExchangeApi(MyAppContext context) : base(context.MessageExchanges)
        {
            this.context = context;
        }


        [Authorize(Roles = "Operator")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("newC", Name = "NewConversation")]
        public IActionResult Create([FromBody] Message message)
        {
            if (!ModelState.IsValid) return BadRequest();


            var messageExchange = NewConversation(message);
            if (messageExchange == null) return BadRequest();

            // Salvo
            context.SaveChanges();//TODO controlla eccezioni

            // Invio come risposta le info dell'utente appena creato
            return CreatedAtRoute("GetME", new { id = messageExchange.Id }, messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        //NOTA: il Name serve per la chiamata da qua dentro.        
        [HttpGet("getME/{id}", Name = "GetMessageExchange")]
        public IActionResult GetById(long id)
        {
            var userId = (User as ClaimsPrincipal)?.FindFirstValue(ClaimTypes.SerialNumber);
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            User user = context.Users.Find(long.Parse(userId));

            var messageExchange = GetMessageExchange(id);
            if (messageExchange == null) return NotFound();

            if (!(user is Consultant || messageExchange.IsThisUserTheOwner(user))) return Unauthorized();

            return Ok(messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [HttpGet("getMELastU", Name = "GetAllMessageExchangesOrderByLastUpdate")]
        public IActionResult GetListOrderedByLastUpdate()
        {
            var messageExchange = base.GetAllMessageExchangesOrderByLastUpdate();
            //var test = context.Messages.ToList();
            //messageExchange = base.GetAllMessageExchangesOrderByLastUpdate();

            if (messageExchange == null) return NotFound();

            return Ok(messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [HttpGet("getMECr", Name = "GetAllMessageExchangeByCreationDate")]
        public IActionResult GetListOrderedByCreationDate()
        {
            var messageExchange = base.GetAllMessageExchangeByCreationDate();

            if (messageExchange == null) return NotFound();

            return Ok(messageExchange);
        }

        [Authorize(Roles = "Operator, Consultant")]
        [HttpPost("addM", Name = "addMessage")]
        public IActionResult AddMessage(long conversationId, [FromBody] Message message)
        {
            var isAdded = base.AddMessageToConversation(conversationId, message);
            if (!isAdded) return BadRequest();

            return CreatedAtRoute("GetMessageExchange", conversationId);
        }

        [Authorize(Roles = "Consultant")]
        [HttpGet("getN/{conversationId}", Name = "getNotes")]
        public IActionResult GetNotesAPI(long conversationId)
        {
            String notes = base.GetNotes(conversationId);
            if (notes == null) return NotFound();

            return Ok(notes);
        }

        [Authorize(Roles = "Consultant")]
        [HttpPost("editN", Name = "editNotes")]
        public IActionResult EditNotes(long conversationId, string text)
        {
            String notes = base.EditNotesInConversation(conversationId, text);
            if (notes == null) return NotFound();

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
