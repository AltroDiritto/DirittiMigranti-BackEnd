using System;
using System.ComponentModel.DataAnnotations;
namespace DirittoMigrantiAPI.Models
{
    public class Operator : User
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string FiscalCode { get; set; }
        [Required]
        public string Job { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Office { get; set; }
        [Required]
        public string Location { get; set; }

        public Operator() : base()
        {
            //TODO
        }

        bool ChangeState(bool newState)
        {
            this.IsActive = newState;
            return this.IsActive;
        }
    }
}
