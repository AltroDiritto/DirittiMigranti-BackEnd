using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.API
{
    internal interface IOperatorAPI
    {
        IActionResult NewOperatorAPI(Operator @operator);
        IActionResult GetOperatorAPI(long userId);
        IActionResult GetAllOperatorsAPI();
        IActionResult SetOperatorStateAPI(long userId, bool newState);
    }
}