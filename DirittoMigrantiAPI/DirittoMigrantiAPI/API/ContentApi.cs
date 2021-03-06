﻿using System;using DirittoMigrantiAPI.Controllers;using DirittoMigrantiAPI.Models;using Microsoft.AspNetCore.Mvc;using System.Linq;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;
using DirittoMigrantiAPI.Contexts;
using Microsoft.AspNetCore.Cors;

namespace DirittoMigrantiAPI.API{    [Route("api/cont")]
    [EnableCors("AllowSpecificOrigin")]    public class ContentApi : ContentController, IPracticeAPI, INewsAPI    {        private readonly MyAppContext context;        public ContentApi(MyAppContext context) : base(context.Contents)        { this.context = context; }

        //A che serve questo metodo?
        //Non presente nell'UML
        //[HttpPost]
        //public IActionResult SetStateAPI(bool newState)
        //{
        //    return BadRequest();
        //}        [HttpGet("getAllNews")]        public IActionResult GetAllNewsAPI(long contentId)        {           var res =context.Contents.Where((c)=> c is News).ToList();                return Ok(res);        }

        [HttpGet("get/{contentId}", Name = "Get")]
        public IActionResult Get(long contentId)        {
            Content c = base.GetContent(contentId);            if (c == null)                return NotFound();            return Ok(c);        }

        #region NEWS        //Tutti possono richiedere news
        [HttpGet("getN/{contentId}", Name = "GetNews")]
        public IActionResult GetNewsAPI(long contentId)        {            News news = base.GetNews(contentId);            if (news == null)                return NotFound();            return Ok(news);        }        [Authorize(Roles = "Consultant")]
        [HttpPost("newN", Name = "NewNews")]        public IActionResult CreateNewsAPI([FromBody] News news)        {            var userId = (User as ClaimsPrincipal)?.FindFirstValue(ClaimTypes.SerialNumber);
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            news.CreationDate = DateTime.Now;            User user = context.Users.Find(long.Parse(userId));
            if (user == null) return BadRequest();

            //TODO IWriter
            news.Writer = (Consultant)user;

            if (!ModelState.IsValid) return BadRequest();

            News newNews = NewNews(news);            if (newNews == null) return BadRequest();            context.SaveChanges();            return Ok(true);        }        [Authorize(Roles = "Consultant")]        [HttpDelete("delN/{contentId}", Name = "DeleteNews")]        public IActionResult DeleteNewsAPI(long contentId)        {
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