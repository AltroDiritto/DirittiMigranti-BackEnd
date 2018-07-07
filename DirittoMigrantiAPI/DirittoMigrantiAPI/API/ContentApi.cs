using System;using DirittoMigrantiAPI.Controllers;using DirittoMigrantiAPI.Models;using DirittoMigrantiAPI.Models.Contexts;using Microsoft.AspNetCore.Mvc;using System.Linq;using Microsoft.AspNetCore.Authorization;namespace DirittoMigrantiAPI.API{    public class ContentApi : ContentController, IPracticeAPI, INewsAPI    {        private readonly ContentContext _context;        public ContentApi(ContentContext context) : base(context.Contents)        { _context = context; }
        //A che serve questo metodo?
        //Non presente nell'UML
        [HttpPost]        public IActionResult SetStateAPI(bool newState)
        {            return BadRequest();        }

        //[Authorize(Roles = "Manager, Administrator")]
        //[HttpPost]
        //private IActionResult CreateContent(Content content)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        View(content);
        //        return BadRequest();
        //    }

        //    //Controllo che l'id sia univoco
        //    if (_context.Contents.Any((c) => c.id == content.id))
        //        return BadRequest();

        //    //Se non scatta BadRequest -> Tutto ok, posso aggiungere
        //    _context.Contents.Add(content);

        //    //Rendo effettivo il tutto
        //    _context.SaveChanges();

        //    return CreatedAtRoute("GetContent", new { id = content.id }, content);
        //}

        //[Authorize(Roles = "Consultant")]
        //[HttpDelete]
        //private IActionResult DeleteContent(long contentId)
        //{
        //    Content content = (Content)_context.Contents.Find(contentId);

        //    if (content == null)
        //        return NotFound();

        //    _context.Contents.Remove(content);

        //    //Rendo effettiva la modifica
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        #region NEWS        //Tutti possono richiedere news
        [HttpGet]
        public IActionResult GetNewsAPI(long contentId)        {            News news = base.GetNews(contentId);            if (news == null)                return NotFound();            return Ok(news);        }        [Authorize(Roles = "Consultant")]        [HttpPost]        public IActionResult CreateNewsAPI([FromBody] News news)        {            if (!ModelState.IsValid)            {                return BadRequest();            }            News checkNews = NewNews(news);            if (checkNews == null)                return BadRequest();            _context.SaveChanges();            return Ok(checkNews);        }        [Authorize(Roles = "Consultant")]        [HttpDelete]        public IActionResult DeleteNewsAPI(long contentId)        {            return Ok(DeleteNews(contentId));        }        [Authorize(Roles = "Consultant")]        [HttpPost]        public IActionResult SetStateAPI(long newsId, bool isPublished)        {            return Ok(SetNewsState(newsId, isPublished));        }
        #endregion
        #region PRACTICE        [HttpGet]
        public IActionResult GetPublicPracticeAPI(long contentId)        {            Practice practice = base.GetPublicPractice(contentId);            if (practice == null)                return NotFound();            return Ok(practice);        }        [Authorize(Roles = "Consultant")]        [HttpGet]        public IActionResult GetPrivatePracticeAPI(long contentId)        {            Practice practice = base.GetPrivatePractice(contentId);            if (practice == null)                return NotFound();            return Ok(practice);        }        [Authorize(Roles = "Consultant")]        [HttpPost]        public IActionResult CreatePracticeAPI(Practice practice)        {            if (!ModelState.IsValid)            {                return BadRequest();            }            Practice checkPractice = NewPractice(practice);            if (checkPractice == null)                return BadRequest();            _context.SaveChanges();            return Ok(NewPractice(practice));        }        [Authorize(Roles = "Consultant")]        [HttpDelete]        public IActionResult DeletePracticeAPI(long contentId)        {            return Ok(DeletePractice(contentId));        }        [Authorize(Roles = "Consultant")]        [HttpPost]        public IActionResult SetPracticePrivacyAPI(long practiceId, bool newState)        {            return Ok(SetPracticePrivacy(practiceId, newState));        }
        #endregion

    }}