using DirittoMigrantiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirittoMigrantiAPI.Controllers
{
    public interface IConversationController
    {
        MessageExchange NewConversation(Message message);
        MessageExchange GetMessageExchange(long MessageExchangeId);
        bool AddMessageToConversation(long MessageExchangeId, Message message);
        string GetNotes(long id);
        string EditNotesInConversation(long MessageExchangeId, string notes);
    }
}
