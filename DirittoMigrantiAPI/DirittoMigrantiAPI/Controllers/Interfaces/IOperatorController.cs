using System;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface IOperatorController
    {
        Operator GetOperator(long userId);
        Operator NewOperator(Operator @operator);
        bool ChangeState(long operatorId, bool newState);
    }
}
