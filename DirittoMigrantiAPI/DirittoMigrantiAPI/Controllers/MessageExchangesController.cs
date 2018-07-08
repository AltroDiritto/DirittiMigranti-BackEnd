using System;
using System.Collections.Generic;
using System.Linq;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class MessageExchangesController : Controller, IConversationHandler, IConversationController
    {
        private readonly DbSet<MessageExchange> messageExchanges;

        public MessageExchangesController(DbSet<MessageExchange> messageExchanges)
        //è lecito che vengano passati dal costruttore?
        {
            this.messageExchanges = messageExchanges;
        }

        #region CONVERSATION CONTROLLER        
        public MessageExchange NewConversation(Message message)
        {
            //TODO sistemare trycatch
            try
            {
                MessageExchange conversation = new MessageExchange(message);
                messageExchanges.Add(conversation);

                return conversation;
            }
            catch (ArgumentException) { return null; }
        }
        
        public MessageExchange GetMessageExchange(long messageExchangeId)
        {
            return messageExchanges.Find(messageExchangeId);
            //return messageExchanges.First(mn => mn.Id == messageExchangeId);
        }

        public bool AddMessageToConversation(long messageExchangeId, Message message)
        {
            var conv = GetMessageExchange(messageExchangeId);

            if (conv == null) return false;

            return conv.AddMessage(message);
        }

        public String GetNotes(long messageExchangeId)
        {
            var conv = GetMessageExchange(messageExchangeId);

            if (conv == null) return null;

            return conv.Notes;
        }

        public string EditNotesInConversation(long messageExchangeId, string notes)
        {
            var conv = GetMessageExchange(messageExchangeId);

            if (conv == null) return null;
            return conv.EditNotes(notes);
        }
        #endregion

        #region CONVERSATION HANDLER        
        public List<MessageExchange> GetConversationsByUser(User user)
        {
            return messageExchanges.Where((conversation) => conversation.IsThisUserInTheConversation(user)).ToList();
        }
        
        public List<MessageExchange> GetConversationsByOwner(User user)
        {
            if (user is Consultant) return null;

            return messageExchanges.Where((conversation) => conversation.IsThisUserTheOwner(user)).ToList();
        }

        public List<MessageExchange> GetAllMessageExchangesOrderByLastUpdate()
        {
            return messageExchanges.OrderBy((conversation) => conversation.GetLastUpdate()).ToList();
            //.ThenBy() starred by user
        }

        public List<MessageExchange> GetAllMessageExchangeByCreationDate()
        {
            throw new NotImplementedException();
        }      
        #endregion

        //LOG
        protected void Log(string message, User user){
            throw new NotImplementedException();
        }

        //Qui molto probabilmente dovrebbe essere "Star"
        public Consultant StartConversation(long conversationId)
        {
            throw new NotImplementedException();
        }

        public List<MessageExchange> GetstarredConversations()
        {
            throw new NotImplementedException();
        }

    }
}
