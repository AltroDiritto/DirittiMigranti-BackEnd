using System;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface IConsultantController
    {
        Consultant GetConsultant(long userId);
    }
}

