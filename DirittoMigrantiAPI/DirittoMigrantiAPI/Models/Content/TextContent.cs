using System;
namespace DirittoMigrantiAPI.Models
{
    public abstract class TextContent
    {
        public readonly User author;
        public readonly DateTime creationDate;
       
        string text;
        string attachmentUrl;
        DateTime lastTextUpdate;

        public TextContent(User author, string text, string attachmentUrl)
        {
            this.author = author;
       
            UpdateText(text);
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }

        private string UpdateText(string text)
        {
            //TODO controllo autore
            this.text = text;
            lastTextUpdate = DateTime.Now;
            return text;
        }
    }
}
