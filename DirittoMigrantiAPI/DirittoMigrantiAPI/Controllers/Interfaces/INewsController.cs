using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface INewsController
    {
        public News GetNews(long id);
        public bool DeleteNews(long contentId);
        public News NewNews();
    }
}