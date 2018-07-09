using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models.Users
{
    public class UserAuth
    {
        [Required]
        [Key]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(200, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        public long UserId { get; set; }

        public UserAuth() { }
        public UserAuth(string username, string password, long userId)
        {
            this.Username = username;
            this.Password = password;
            this.UserId = userId;
        }

        public bool CheckCredentials(UserAuth userAuth)
        {
            return (Username.Equals(userAuth.Username) && Password.Equals(userAuth.Password));

        }

        public bool CheckPassword(string password){
            return Password.Equals(password);
        }


    }
}
