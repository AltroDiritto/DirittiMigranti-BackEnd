using System;//nuova news
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class ContentController : Controller, INewsController, IPracticeController
    {
        private readonly DbSet<Content> contents;

        //DA QUI PARTONO TUTTI I METODI IN COMUNE CON L'UML
        public ContentController(DbSet<Content> contents)
        {
            this.contents = contents;
        }

        private Content GetContent(long id)
        {
            return contents.Find(id);
        }

        private Content NewContent(Content textContent)
        {
            if (textContent == null) return null;

            contents.Add(textContent);
            return textContent;
        }

        private bool DeleteContent(long contentId)
        {
            throw new NotImplementedException();
        }
                   
        #region NEWS 
        public News GetNews(long id)
        {
            throw new NotImplementedException();
        }

        public News NewNews(News news)
        {
            throw new NotImplementedException();
        }

        public bool SetState(long contentId, bool isPublished)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNews(long contentId)
        {
            var content = contents.Find(contentId);
            contents.Remove(content);
            var check = contents.Find(contentId);
            return check != null;
        }
        #endregion
        
        #region PRACTICE        
        public Practice GetPractice(long contentId)
        {
            throw new NotImplementedException();
        }

        public Practice NewPractice(Practice practice)
        {
            throw new NotImplementedException();
        }

        public bool SetPracticePrivacy(long contentId, bool newState)
        {
            throw new NotImplementedException();
        }

        public bool DeletePractice(long contentId)
        {
            var content = contents.Find(contentId);
            contents.Remove(content);
            var check = contents.Find(contentId);
            return check != null;
        }        
        #endregion

        //LOG
        public void Log(string message, User user)
        {
            throw new NotImplementedException();
        }










    }
}
