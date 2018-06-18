using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DirittoMigrantiAPI.Models
{
    public class Consultant : User
    {
        List<MessageExchange> starredConversations;

        public Consultant() : base()
        {
            //da caricare da db
            starredConversations = new List<MessageExchange>();
        }

        public void StarConversation(MessageExchange messageExchange)
        {
            starredConversations.Add(messageExchange);
        }

        public void UnstarConversation(MessageExchange messageExchange)
        {
            if (starredConversations.Contains(messageExchange))
                starredConversations.Remove(messageExchange);
        }
    }
}
