using System;
namespace DirittoMigrantiAPI.Models
{
    public class Message
    {
        public readonly User author;
        public readonly DateTime creationDate;
        readonly string messageContent;

        string attachmentUrl;

        public Message(User author, string messageContent)
        {
            this.author = author;
            this.messageContent = messageContent;
            this.attachmentUrl = "";

            creationDate = DateTime.Now;
        }

        public Message(User author, string messageContent, string attachmentUrl)
        {
            this.author = author;
            this.messageContent = messageContent;
            this.attachmentUrl = attachmentUrl;

            creationDate = DateTime.Now;
        }

        //Edit message
    }
}
