using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirittoMigrantiAPI.API
{
    interface INewsAPI
    {
        IActionResult GetNewsAPI(long contentId);
        IActionResult CreateNewsAPI(News news);
        IActionResult DeleteNewsAPI(long contentId);
        IActionResult SetStateAPI(long newsId, bool isPublished);
    }
}
