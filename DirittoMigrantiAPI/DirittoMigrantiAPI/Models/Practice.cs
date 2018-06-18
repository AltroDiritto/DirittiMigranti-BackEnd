using System;
namespace DirittoMigrantiAPI.Models
{
    public class Practice
    {
        public readonly Consultant author;
        public readonly DateTime creationDate;
        bool isPrivate;
        string title;
        string text;
        string attachmentUrl;
        DateTime lastTextUpdate;

        public Practice(Consultant author, string title, string text, bool isPrivate)
        {
            this.author = author;
            this.title = title;
            UpdateText(text);
            this.isPrivate = isPrivate;
            this.attachmentUrl = "";
            creationDate = DateTime.Now;
        }

        public Practice(Consultant author, string title, string text,string attachmentUrl, bool isPrivate)
        {
            this.author = author;
            this.title = title;
            UpdateText(text);
            this.isPrivate = isPrivate;
            this.attachmentUrl = attachmentUrl;
            creationDate = DateTime.Now;
        }

        public string UpdateText(string text){
            this.text = text;
            lastTextUpdate= DateTime.Now;
            return text;
        }

        public void ChangePrivacy(bool newPrivacy){
            this.isPrivate = newPrivacy;
        }
    }
}
