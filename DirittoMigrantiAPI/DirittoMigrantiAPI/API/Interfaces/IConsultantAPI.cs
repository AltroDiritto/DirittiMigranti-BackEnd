using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.API
{
    internal interface IConsultantAPI
    {
        IActionResult GetConsultantAPI(long userId);
    }
}