using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models
{
    public class Practice : Content
    {
        [Required]
        bool IsPrivate { get; set; }

        public Practice() { }

        public Practice(Consultant writer, string title, string text, bool isPrivate) : base(writer, title, text, "")
        {
            this.IsPrivate = isPrivate;
        }

        public Practice(Consultant writer, string title, string text, string attachmentUrl, bool isPrivate) : base(writer, title, text, attachmentUrl)
        {
            this.IsPrivate = isPrivate;
        }

        public void ChangePrivacy(bool newPrivacy)
        {
            this.IsPrivate = newPrivacy;
        }

        public bool IsThisPrivate()
        {
            return IsPrivate;
        }



    }
}
