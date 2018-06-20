using System;
namespace DirittoMigrantiAPI.Models
{
    public class News : TextContent
    {
        string title;
        bool isPublished;
        //comments

        public News(Consultant author, string title, string text) : base(author, text, "")
        {
            this.title = title;
            isPublished = false;
        }

        public News(Consultant author, string title, string text, string attachmentUrl) : base(author, text, attachmentUrl)
        {
            this.title = title;
            isPublished = false;
        }

        public void Publish(){
            isPublished = true;
        }

        public void Hide()
        {
            isPublished = false;
        }
    }
}
