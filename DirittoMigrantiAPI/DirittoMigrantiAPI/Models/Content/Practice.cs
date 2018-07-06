using System;
namespace DirittoMigrantiAPI.Models
{
    public class Practice : Content
    {
        bool isPrivate;

        public Practice(Consultant writer, string title, string text, bool isPrivate) : base(writer, title, text, "")
        {
            this.isPrivate = isPrivate;
        }

        public Practice(Consultant writer, string title, string text, string attachmentUrl, bool isPrivate) : base(writer, title, text, attachmentUrl)
        {
            this.isPrivate = isPrivate;
        }

        public void ChangePrivacy(bool newPrivacy)
        {
            this.isPrivate = newPrivacy;
        }

        public bool IsPrivate()
        {
            return isPrivate;
        }



    }
}
