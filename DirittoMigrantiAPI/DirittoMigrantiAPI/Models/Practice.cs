using System;
namespace DirittoMigrantiAPI.Models
{
    public class Practice : TextContent
    {
        string title;
        bool isPrivate;

        public Practice(Consultant author, string title, string text, bool isPrivate) : base(author, text, "")
        {
            this.title = title;
            this.isPrivate = isPrivate;
        }

        public Practice(Consultant author, string title, string text, string attachmentUrl, bool isPrivate) : base(author, text, attachmentUrl)
        {
            this.title = title;
            this.isPrivate = isPrivate;
        }

        public void ChangePrivacy(bool newPrivacy)
        {
            this.isPrivate = newPrivacy;
        }
    }
}
