using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.API
{
    internal interface IConsultantAPI
    {
        IActionResult GetConsultant(long userId);
    }
}