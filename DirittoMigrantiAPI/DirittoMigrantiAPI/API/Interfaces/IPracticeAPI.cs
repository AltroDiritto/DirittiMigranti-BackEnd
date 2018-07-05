using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirittoMigrantiAPI.API
{
    public interface IPracticeAPI
    {
        IActionResult GetPracticeAPI(long contentId);
        IActionResult CreatePracticeAPI(Practice practice);
        IActionResult DeletePracticeAPI(long contentId);
        IActionResult SetPracticePrivacyAPI(long practiceId, bool newState);
    }
}
