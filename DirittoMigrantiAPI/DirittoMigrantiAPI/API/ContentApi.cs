using System;using DirittoMigrantiAPI.Controllers;using DirittoMigrantiAPI.Models;using Microsoft.AspNetCore.Mvc;using System.Linq;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;
using DirittoMigrantiAPI.Contexts;

namespace DirittoMigrantiAPI.API{    [Route("api/cont")]    public class ContentApi : ContentController, IPracticeAPI, INewsAPI    {        private readonly MyAppContext context;        public ContentApi(MyAppContext context) : base(context.Contents)        { this.context = context; }
        //A che serve questo metodo?
        //Non presente nell'UML
        //[HttpPost]        //public IActionResult SetStateAPI(bool newState)
        //{        //    return BadRequest();        //}

        #region NEWS        //Tutti possono richiedere news
        [HttpGet("getN/{contentId}", Name = "GetNews")]
        public IActionResult GetNewsAPI(long contentId)        {            News news = base.GetNews(contentId);            if (news == null)                return NotFound();            return Ok(news);        }        [Authorize(Roles = "Consultant")]
        [HttpPost("newN", Name = "NewNews")]        public IActionResult CreateNewsAPI([FromBody] News news)        {            var userId = (User as ClaimsPrincipal)?.FindFirstValue(ClaimTypes.SerialNumber);
            if (string.IsNullOrEmpty(userId)) return BadRequest();// TODO aggiungere ovunque

            news.CreationDate = DateTime.Now;            news.Writer = (Consultant)Startup.users[1];//TODO da sistemare!!!!!!!!!!!!!!!!!!!!

            if (!ModelState.IsValid) return BadRequest();

            News checkNews = NewNews(news);            if (checkNews == null) return BadRequest();            context.SaveChanges();            return Ok(checkNews);        }        [Authorize(Roles = "Consultant")]        [HttpDelete("delN/{contentId}", Name = "DeleteNews")]        public IActionResult DeleteNewsAPI(long contentId)        {
            var ris = DeleteNews(contentId);            context.SaveChanges();            return Ok(ris);        }

        //[Authorize(Roles = "Consultant")]
        [HttpPost("setN/{newsId}/{isPublished}", Name = "SetNews")]//TODO ricevere parametri
        public IActionResult SetNewsStateAPI(long newsId, bool isPublished)        {            return Ok(SetNewsState(newsId, isPublished));        }
        #endregion
        #region PRACTICE        [HttpGet("getPuP/{contentId}", Name = "GetPublicPractice")]
        public IActionResult GetPublicPracticeAPI(long contentId)        {            Practice practice = base.GetPublicPractice(contentId);            if (practice == null)                return NotFound();            return Ok(practice);        }        [Authorize(Roles = "Consultant, Operator")]
        [HttpGet("getPrP/{contentId}", Name = "GetPrivatePractice")]        public IActionResult GetPrivatePracticeAPI(long contentId)        {            Practice practice = base.GetPrivatePractice(contentId);            if (practice == null)                return NotFound();            return Ok(practice);        }        [Authorize(Roles = "Consultant")]        [HttpPost("newP", Name = "NewPractice")]        public IActionResult CreatePracticeAPI([FromBody] Practice practice)        {            if (!ModelState.IsValid)            {                return BadRequest();            }            Practice checkPractice = NewPractice(practice);            if (checkPractice == null)                return BadRequest();            context.SaveChanges();            return Ok(NewPractice(practice));        }        [Authorize(Roles = "Consultant")]        [HttpDelete("delP/{contentId}", Name = "DeletePractice")]        public IActionResult DeletePracticeAPI(long contentId)        {
            var ris = DeletePractice(contentId);

            context.SaveChanges();            return Ok(ris);        }        [Authorize(Roles = "Consultant")]        [HttpPost("setP/{practiceId}/{newState}", Name = "SetPracticePrivacy")]        public IActionResult SetPracticePrivacyAPI(long practiceId, bool newState)        {            return Ok(SetPracticePrivacy(practiceId, newState));        }
        #endregion

    }}