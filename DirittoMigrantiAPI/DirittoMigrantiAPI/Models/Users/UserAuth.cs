using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models.Users
{
    public class UserAuth
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
