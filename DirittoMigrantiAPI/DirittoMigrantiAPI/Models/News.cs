using System;
namespace DirittoMigrantiAPI.Models
{
    public class News
    {
        public readonly Consultant author;
        public readonly DateTime creationDate;
        string title;
        string text;
        string attachmentUrl;
        DateTime lastTextUpdate;

        public News(Consultant author, string title, string text)
        {
            this.author = author;
            this.title = title;
            this.text = text;
            this.attachmentUrl = "";
            creationDate = DateTime.Now;
        }

        public News(Consultant author, string title, string text,string attachmentUrl)
        {
            this.author = author;
            this.title = title;
            this.text = text;
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }
    }
}
