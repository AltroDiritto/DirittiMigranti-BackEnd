﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models
{
    public class Content
    {
        // Id used as a key in the dictionary where all the users are stored
        [Key]
        public long Id { get; set; }
        [Required]
        public User Writer { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]        
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000,MinimumLength = 0)]
        string Text { get; set; }
        [Url]
        string AttachedURL { get; set; }

        DateTime lastTextUpdate;

        public Content() { }

        public Content(User writer, string title, string text, string attachmentUrl)
        {
            this.Writer = writer;
            this.Title = title;

            UpdateText(text);
            this.AttachedURL = attachmentUrl;
            CreationDate = DateTime.Now;
        }

        private string UpdateText(string text)
        {
            //TODO controllo autore
            this.Text = text;
            lastTextUpdate = DateTime.Now;
            return this.Text;
        }
    }
}
