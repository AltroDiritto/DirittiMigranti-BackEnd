using System;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Contexts;

namespace DirittoMigrantiAPI.API
{
    public class ContentApi : ContentController
    {
        private readonly ContentContext _context;
        public ContentApi(ContentContext context) : base(context.Contents)
        { _context = context; }


    }
}
