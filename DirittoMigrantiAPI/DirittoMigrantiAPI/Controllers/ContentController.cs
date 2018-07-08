using System;//nuova news
using System.Linq;
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

        protected Content GetContent(long id)
        {
            return contents.Find(id);
        }

        protected Content NewContent(Content textContent)
        {
            if (textContent == null) return null;

            contents.Add(textContent);
            return textContent;
        }

        private bool DeleteContent(long contentId)
        {
            var content = GetContent(contentId);
            if (content == null) return false;
            contents.Remove(content);
            var check = GetContent(contentId);
            return check != null;

        }

        #region NEWS 
        public News GetNews(long id)
        {
            var content = GetContent(id);
            if (content == null || !(content is News)) return null;

            News news = (News)content;
            if (!news.IsPublished) return null;
            return news;
        }

        public News NewNews(News news)
        {
            return (News)NewContent(news);
        }

        public bool SetNewsState(long contentId, bool isPublished)
        {
            News news = GetNews(contentId);
            if (news == null) return false;
            if (isPublished)
                news.Publish();
            else news.Hide();
            return news.IsPublished;
        }

        public bool DeleteNews(long contentId)
        {
            //serve controllare se il contenuto è effettivamente una news?
            return DeleteContent(contentId);
        }
        #endregion

        #region PRACTICE        
        public Practice GetPublicPractice(long contentId)
        {
            var content = GetContent(contentId);

            if (content ==null || !(content is Practice)) return null;//TODO farlo ovunque

            Practice practice = (Practice)content;
            if (practice.IsThisPrivate())
                return null;
            return practice;
        }


        public Practice GetPrivatePractice(long contentId)
        {
            var content = GetContent(contentId);
            if (content == null || !(content is Practice)) return null;

            Practice practice = (Practice)content;           
            return practice;
        }


        public Practice NewPractice(Practice practice)
        {
            return (Practice)NewContent(practice);
        }

        public bool SetPracticePrivacy(long contentId, bool newState)
        {
            Practice practice = (Practice)GetContent(contentId);
            practice.ChangePrivacy(newState);
            return newState;
        }

        public bool DeletePractice(long contentId)
        {
            return DeleteContent(contentId);
        }
        #endregion

        //LOG
        public void Log(string message, User user)
        {
            throw new NotImplementedException();
        }
    }
}
