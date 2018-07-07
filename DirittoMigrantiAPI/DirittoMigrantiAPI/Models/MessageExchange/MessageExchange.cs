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
        public string Notes { get; private set; }
    
        private List<Message> messages = new List<Message>();


        public MessageExchange(Message message)
        {
            if (!(message.Author is Operator)) throw new ArgumentException("");

            //se il messaggio è di un consulente, crasha il cast
            conversationOwner = (Operator)message.Author;
            messages.Add(message);
            creationDate = DateTime.Now;
        }

        public bool AddMessage(Message message)
        {
            if (message.Author == conversationOwner || message.Author is Consultant)
            {
                messages.Add(message);
                return true;
            }
            return false;
        }

        public string EditNotes(string notes)
        {
            //viene controllato nel controller prima di essere chiamato
            this.Notes = notes;
            return notes;
        }

        public DateTime? GetLastUpdate()
        {
            if (messages != null)
                if (messages.Count > 0)
                    return messages[messages.Count - 1].CreationDate;
            return null;
        }

        public bool IsThisUserInTheConversation(User user)
        {
            return messages.Any((message) => message.Author == user);
        }

        public bool IsThisUserTheOwner(User user)
        {
            return conversationOwner == user;
        }
    }
}

