using System;
using System.Collections.Generic;
using System.Linq;

namespace DirittoMigrantiAPI.Models
{
    public class MessageExchange
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        readonly public Operator owner;
        string notes="";
        private List<Message> messages = new List<Message>();


        public MessageExchange(Message message)
        {
            messages.Add(message);
            owner = (Operator)message.author;
        }

        public bool AddMessage(Message message)
        {
            if (message.author == owner || message.author is Operator)
            {
                messages.Add(message);
                return true;
            }
            return false;
        }

        public string EditNotes(string notes)
        {

            this.notes = notes;
            return notes;
        }

        DateTime? GetLastUpdate()
        {
            if (messages?.Count > 0)
                return messages[messages.Count - 1].creationDate;
            return null;
        }

        public bool IsThisUserInTheConversation(User user)
        {
            return messages.Any((message) => message.author == user);
        }

        public bool IsThisUserTheOwner(User user)
        {
            return owner == user;
        }
    }
}

