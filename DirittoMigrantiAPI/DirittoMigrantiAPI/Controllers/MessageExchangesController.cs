using System.Collections.Generic;
using System.Linq;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class MessageExchangesController : Controller
    {
        private readonly DbSet<MessageExchange> messageExchanges;

        public MessageExchangesController(DbSet<MessageExchange> messageExchanges)
        //è lecito che vengano passati dal costruttore?
        { this.messageExchanges = messageExchanges; }

        protected MessageExchange NewConversation(Message message)
        {
            if (!(message.author is Operator)) return null;//todo gestire

            MessageExchange conversation = new MessageExchange(message);
            messageExchanges.Add(conversation);

            return conversation;
        }

        protected MessageExchange GetMessageExchange(long MessageExchangeId)
        {
            return messageExchanges.Find(MessageExchangeId);
        }

        protected MessageExchange GetMessageExchange(User user, long MessageExchangeId)
        {
            MessageExchange messageExchange = GetMessageExchange(MessageExchangeId);
            //TODO controllare chi lo chiama
            return messageExchange;
        }

        protected List<MessageExchange> GetAllMessageExchangesByLastUpdate()
        {
            return messageExchanges.OrderBy((conversation) => conversation.GetLastUpdate()).ToList();
            //.ThenBy() starred by user
        }

        protected List<MessageExchange> GetConversationsByUser(User user)
        {
            return messageExchanges.Where((conversation) => conversation.IsThisUserInTheConversation(user)).ToList();
        }

        protected List<MessageExchange> GetConversationsByOwner(User user)
        {
            if (user is Consultant) return null;

            return messageExchanges.Where((conversation) => conversation.IsThisUserTheOwner(user)).ToList();
        }

        protected bool AddMessageToConversation(long MessageExchangeId, Message message)
        {
            //TODO controllare chi lo chiama
            return GetMessageExchange(MessageExchangeId).AddMessage(message);
        }

        protected string EditNotesInConversation(long MessageExchangeId, string notes)
        {
            return GetMessageExchange(MessageExchangeId).EditNotes(notes);
        }
    }
}
