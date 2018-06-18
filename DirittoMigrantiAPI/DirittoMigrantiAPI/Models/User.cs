using System.ComponentModel.DataAnnotations;
namespace DirittoMigrantiAPI.Models

{
    //https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx
    //https://docs.microsoft.com/it-it/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-2.0

    //[Bind(Exclude = "Id")]
    public class User
    {
        // Id used as a key in the dictionary where all the users are stored
        public long Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Surname { get; set; }
    }
}