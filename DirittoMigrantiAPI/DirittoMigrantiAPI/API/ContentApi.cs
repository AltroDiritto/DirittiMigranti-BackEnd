using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DirittoMigrantiAPI.API
{
    public class ContentApi : ContentController
    {
        private readonly ContentContext _context;
        public ContentApi(ContentContext context) : base(context.Contents)
        { _context = context; }


        [HttpPost]
        public IActionResult SetState(bool newState){



            return BadRequest();
        }

    }
}
