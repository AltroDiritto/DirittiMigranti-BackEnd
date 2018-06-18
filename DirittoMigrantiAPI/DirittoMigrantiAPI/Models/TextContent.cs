using System;
namespace DirittoMigrantiAPI.Models
{
    public abstract class TextContent
    {
        public readonly User author;
        public readonly DateTime creationDate;
        string title;
        string text;
        string attachmentUrl;
        DateTime lastTextUpdate;

        public TextContent(User author, string title, string text, string attachmentUrl)
        {
            this.author = author;
            this.title = title;
            UpdateText(text);
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }

        public string UpdateText(string text)
        {
            this.text = text;
            lastTextUpdate = DateTime.Now;
            return text;
        }
    }
}
