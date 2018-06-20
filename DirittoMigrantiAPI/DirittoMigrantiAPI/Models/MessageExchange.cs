using System;
using System.Collections.Generic;
using System.Linq;

namespace DirittoMigrantiAPI.Models
{
    public class MessageExchange
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        public readonly DateTime creationDate;
        readonly public Operator conversationOwner;
        string notes = "";
        private List<Message> messages = new List<Message>();


        public MessageExchange(Message message)
        {
            messages.Add(message);
            //se il messaggio è di un consulente, crasha il cast
            conversationOwner = (Operator)message.author;
            creationDate = DateTime.Now;
        }

        public bool AddMessage(Message message)
        {
            if (message.author == conversationOwner || message.author is Consultant)
            {
                messages.Add(message);
                return true;
            }
            return false;
        }

        public string EditNotes(string notes)
        {
            //viene controllato nel controller prima di essere chiamato
            this.notes = notes;
            return notes;
        }

        public DateTime? GetLastUpdate()
        {
            if (messages != null)
                if (messages.Count > 0)
                    return messages[messages.Count - 1].creationDate;
            return null;
        }

        public bool IsThisUserInTheConversation(User user)
        {
            return messages.Any((message) => message.author == user);
        }

        public bool IsThisUserTheOwner(User user)
        {
            return conversationOwner == user;
        }
    }
}

