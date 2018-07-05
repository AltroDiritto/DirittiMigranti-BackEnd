using System;using DirittoMigrantiAPI.Controllers;using DirittoMigrantiAPI.Models;using DirittoMigrantiAPI.Models.Contexts;using Microsoft.AspNetCore.Mvc;namespace DirittoMigrantiAPI.API{    public class ContentApi : ContentController, IPracticeAPI, INewsAPI    {        private readonly ContentContext _context;        public ContentApi(ContentContext context) : base(context.Contents)        { _context = context; }


        //Non presente nell'UML
        [HttpPost]        public IActionResult SetStateAPI(bool newState)
        {            return BadRequest();        }

        //Da qui ci sono TUTTI i metodi presenti nell'UML
        private IActionResult GetContent(Content content)        {            throw new NotImplementedException();        }        private IActionResult CreateContent(long contentId)        {            throw new NotImplementedException();        }        private IActionResult DeleteContent(long contentId)        {            throw new NotImplementedException();        }

        #region NEWS
        public IActionResult GetNewsAPI(long contentId)        {            throw new NotImplementedException();        }        public IActionResult CreateNewsAPI(News news)        {            throw new NotImplementedException();        }        public IActionResult DeleteNewsAPI(long contentId)        {            throw new NotImplementedException();        }        public IActionResult SetStateAPI(long newsId, bool isPublished)        {            throw new NotImplementedException();        }
        #endregion
        #region PRACTICE
        public IActionResult GetPracticeAPI(long contentId)        {            throw new NotImplementedException();        }        public IActionResult CreatePracticeAPI(Practice practice)        {            throw new NotImplementedException();        }        public IActionResult DeletePracticeAPI(long contentId)        {            throw new NotImplementedException();        }        public IActionResult SetPracticePrivacyAPI(long practiceId, bool newState)        {            throw new NotImplementedException();        }
        #endregion

    }}