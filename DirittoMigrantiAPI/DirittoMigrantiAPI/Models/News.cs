using System;
namespace DirittoMigrantiAPI.Models
{
    public class News : TextContent
    {
        bool isPublished;
        //comments

        public News(Consultant author, string title, string text) : base(author, title, text, "")
        {
            isPublished = false;
        }

        public News(Consultant author, string title, string text, string attachmentUrl) : base(author, title, text, attachmentUrl)
        {
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
