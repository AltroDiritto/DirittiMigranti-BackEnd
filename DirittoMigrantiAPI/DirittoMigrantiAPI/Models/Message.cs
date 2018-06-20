using System;
namespace DirittoMigrantiAPI.Models
{
    public class Message : TextContent
    {
        public Message(User author, string text) : base(author, text, "")
        {
        }

        public Message(User author, string text, string attachmentUrl) : base(author, text, attachmentUrl)
        {
        }

        //Edit message
    }
}
