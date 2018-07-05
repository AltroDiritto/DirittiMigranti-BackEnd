using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.API
{
    internal interface IOperatorAPI
    {
        IActionResult NewOperator(Operator @operator);
        IActionResult GetOperator(long userId);
        IActionResult GetAllOperators();
        IActionResult SetOperatorState(long userId, bool newState);
    }
}