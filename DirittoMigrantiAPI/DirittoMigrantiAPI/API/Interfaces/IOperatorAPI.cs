using Microsoft.AspNetCore.Mvc;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;

namespace DirittoMigrantiAPI.API
{
    internal interface IOperatorAPI
    {
        IActionResult NewOperatorAPI(UserAuth ua, Operator @operator);
        IActionResult GetOperatorAPI(long userId);
        IActionResult GetAllOperatorsAPI();
        IActionResult SetOperatorStateAPI(long userId, bool newState);
    }
}