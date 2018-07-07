using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models

{
    public class Message
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        [Required]
        public User Author { get; set; }
        [Required]
        [DisplayFormat(DataFormatString="{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        public DateTime CreationDate { get; set; }
        [Required]
        [StringLength(400)]
        public string MessageContent { get; set; }
        [Url]
        public string AttachmentURL { get; set; }

        public MessageExchange WhereIBelong { get; set; }

        public Message(User author, string messageContent)
        {
            this.Author = author;
            this.MessageContent = messageContent;
            this.AttachmentURL = "";

            CreationDate = DateTime.Now;
        }

        public Message(User author, string messageContent, string attachmentUrl)
        {
            this.Author = author;
            this.MessageContent = messageContent;
            this.AttachmentURL = attachmentUrl;

            CreationDate = DateTime.Now;
        }

        //Edit message
    }
}
