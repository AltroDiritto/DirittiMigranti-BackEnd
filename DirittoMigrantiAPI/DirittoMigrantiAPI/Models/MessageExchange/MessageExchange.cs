using System;
using System.Collections.Generic;
using System.Linq;

namespace DirittoMigrantiAPI.Models
{
    public class MessageExchange
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        public DateTime creationDate { get; set; }
        public Operator conversationOwner { get; set; }
        public string Notes { get; private set; }

        public ICollection<Message> Messages { get ; set; }

        public MessageExchange() { Messages = new List<Message>(); }

        public MessageExchange(Message message)
        {
            Messages = new List<Message>();//TODO vedere come farlo meglio

            if (!(message.Author is Operator)) throw new ArgumentException("");

            //se il messaggio è di un consulente, crasha il cast
            conversationOwner = (Operator)message.Author;
            Messages.Add(message);
            creationDate = DateTime.Now;
        }

        public bool AddMessage(Message message)
        {
            if (message.Author == conversationOwner || message.Author is Consultant)
            {
                Messages.Add(message);
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
            if (Messages != null)
                if (Messages.Count > 0)
                    return Messages.Last().CreationDate;
            return null;
        }

        public bool IsThisUserInTheConversation(User user)
        {
            return Messages.Any((message) => message.Author == user);
        }

        public bool IsThisUserTheOwner(User user)
        {
            return conversationOwner == user;
        }
    }
}

