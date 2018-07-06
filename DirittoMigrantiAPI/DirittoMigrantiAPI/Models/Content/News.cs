using System;
namespace DirittoMigrantiAPI.Models
{
    public class News : Content
    {
        public bool IsPublished { get; private set; }

        public News() { }

        public News(Consultant writer, string title, string text) : base(writer, title, text, "")
        {
            IsPublished = false;
        }

        public News(Consultant writer, string title, string text, string attachmentUrl) : base(writer, title, text, attachmentUrl)
        {
            IsPublished = false;
        }

        public void Publish()
        {
            IsPublished = true;
        }

        public void Hide()
        {
            IsPublished = false;
        }
    }
}
