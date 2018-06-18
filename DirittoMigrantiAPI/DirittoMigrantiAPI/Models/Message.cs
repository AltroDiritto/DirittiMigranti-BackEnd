using System;
namespace DirittoMigrantiAPI.Models
{
    public class Message
    {
        public readonly User author;
        public readonly DateTime creationDate;
        string text;
        string attachmentUrl;

        public Message(User author, string text)
        {
            this.author = author;
            this.text = text;
            this.attachmentUrl = "";
            creationDate = DateTime.Now;
        }

        public Message(User author, string text, string attachmentUrl)
        {
            this.author = author;
            this.text = text;
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }

    }
}
