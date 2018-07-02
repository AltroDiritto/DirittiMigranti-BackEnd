using System;
namespace DirittoMigrantiAPI.Models
{
    public abstract class Content
    {
        public readonly int id;
        public readonly User writer;
        public readonly DateTime creationDate;

        string title;
        string text;
        string attachmentUrl;

        DateTime lastTextUpdate;

        public Content(User writer, string title, string text, string attachmentUrl)
        {
            this.writer = writer;
            this.title = title;

            UpdateText(text);
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }

        private string UpdateText(string text)
        {
            //TODO controllo autore
            this.text = text;
            lastTextUpdate = DateTime.Now;
            return this.text;
        }
    }
}
