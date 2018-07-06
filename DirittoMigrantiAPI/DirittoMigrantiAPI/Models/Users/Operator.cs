using System;
using System.ComponentModel.DataAnnotations;
namespace DirittoMigrantiAPI.Models
{
    public class Operator : User
    {
        //  [Required]
        public bool IsActive { get; set; }
        // [Required]
        public string FiscalCode { get; set; }
        ///[Required]
        public string Job { get; set; }
        // [Required]
        [Phone]
        public string Phone { get; set; }
        // [Required]
        public string Office { get; set; }
        //[Required]
        
        public string Location { get; set; }

        public Operator() : base() { }

        public Operator(string name, string surname, string email, string fiscalCode, string job, string location) : base()
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.FiscalCode = fiscalCode;
            this.Job = job;
            this.Location = location;
        }

        public bool ChangeState(bool newState)
        {
            this.IsActive = newState;

            return this.IsActive;
        }
    }
}
