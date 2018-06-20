using System.Collections.Generic;
using System.Linq;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DirittoMigrantiAPI.Controllers
{
    public class MessageExchangesController : Controller
    {
        private readonly MessageExchangesContext _context;
        public MessageExchangesController(MessageExchangesContext context) { _context = context; }

        private MessageExchange NewConversation(Message message)
        {
            if (!(message.author is Operator)) return null;//todo gestire

            MessageExchange conversation = new MessageExchange(message);
            _context.MessageExchanges.Add(conversation);
            _context.SaveChanges();

            return conversation;
        }

        private MessageExchange GetMessageExchange(User user, long MessageExchangeId)
        {
            //TODO controllare chi lo chiama
            MessageExchange messageExchange = _context.MessageExchanges.Find(MessageExchangeId);
            return messageExchange;
        }

        private List<MessageExchange> GetAllMessageExchangesByLastUpdate()
        {
            return _context.MessageExchanges.OrderBy((conversation) => conversation.GetLastUpdate()).ToList();
            //.ThenBy() starred by user
        }

        private List<MessageExchange> GetConversationsByUser(User user)
        {
            return _context.MessageExchanges.Where((conversation) => conversation.IsThisUserInTheConversation(user)).ToList();
        }

        private List<MessageExchange> GetConversationsByOwner(User user)
        {
            return _context.MessageExchanges.Where((conversation) => conversation.IsThisUserTheOwner(user)).ToList();
        }

        private bool AddMessageToConversation(long MessageExchangeId, Message message)
        {
            //TODO controllare chi lo chiama
            return _context.MessageExchanges.Find(MessageExchangeId).AddMessage(message);
        }

        private string EditNotesInConversation(long MessageExchangeId, string notes)
        {
            return _context.MessageExchanges.Find(MessageExchangeId).EditNotes(notes);
        }
    }
}
