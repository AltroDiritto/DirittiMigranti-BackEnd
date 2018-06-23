using System;//nuova news
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Controllers
{
    public class ContentController : Controller//, INewsController
    {
        private readonly DbSet<Content> contents;
        public ContentController(DbSet<Content> contents)
        {
            this.contents = contents;
        }

        private Content GetTextContent(long id)
        {
            return contents.Find(id);
        }

        private Content NewTextContent(Content textContent)
        {
            if (textContent == null) return null;

            contents.Add(textContent);
            return textContent;
        }

        #region News
        protected News GetNews(long id)
        {
            return (News)GetTextContent(id);
        }

        protected News NewNews()//TODO add parameters
        {
            News news = new News(null, null, null);
            return (News)NewTextContent(news);
        }
        #endregion

        #region Practice
        protected Practice GetPractice(long id)
        {
            return (Practice)contents.Find(id);
        }

        protected Practice NewPractice()//TODO add parameters
        {
            Practice practice = new Practice(null, null, null, false);
            contents.Add(practice);
            return practice;
        }

        protected Practice EditState(long id, bool newState)
        {
            Practice practice = (Practice)contents.Find(id);
            practice.ChangePrivacy(newState);
            return practice;
        }

        #endregion

        //edit news
        //edit pratica

        //cancella news
        //cancella pratica

    }
}
