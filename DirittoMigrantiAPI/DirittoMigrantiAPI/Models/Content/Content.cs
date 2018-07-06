using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models
{
    public class Content
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        public readonly User writer;
        public readonly DateTime creationDate;
                
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }
        string text;
        string attachmentUrl;

        DateTime lastTextUpdate;

        public Content() { }

        public Content(User writer, string title, string text, string attachmentUrl)
        {
            this.writer = writer;
            this.Title = title;

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
