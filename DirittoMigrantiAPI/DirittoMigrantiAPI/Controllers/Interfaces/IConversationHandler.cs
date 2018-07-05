using DirittoMigrantiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirittoMigrantiAPI.Controllers
{
    public interface IConversationHandler
    {
        List<MessageExchange> GetConversationsByUser(User user);
        List<MessageExchange> GetConversationsByOwner(User user);
        List<MessageExchange> GetAllMessageExchangesOrderByLastUpdate();
        List<MessageExchange> GetAllMessageExchangeByCreationDate();
    }
}
