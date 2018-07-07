using System;
using System.ComponentModel.DataAnnotations;

namespace DirittoMigrantiAPI.Models.Users
{
    public class UserAuth
    {        
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        [Required]
        [Key]
        [StringLength(30, MinimumLength = 5)]
        public string Username {  get; set; }

        [StringLength(40, MinimumLength = 8)]
        [Required]
        public string Password {  get; set; }

        public long UserId { get; set; }


        public UserAuth(){}
        public UserAuth(string username,string password,long userId){
            Username = username;
            Password = password;
            UserId = UserId;
        }

        public bool CheckCredentials(UserAuth userAuth)
        {
            return (Username.Equals(userAuth.Username) && Password.Equals(userAuth.Password));

        }


    }
}
