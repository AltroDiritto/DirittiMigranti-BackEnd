using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models
{
    public class TokenRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
