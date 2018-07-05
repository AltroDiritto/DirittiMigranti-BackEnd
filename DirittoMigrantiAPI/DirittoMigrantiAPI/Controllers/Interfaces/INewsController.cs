using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface INewsController
    {
        News GetNews(long id);
        bool DeleteNews(long contentId);
        News NewNews(News news);
        bool SetState(long contendId, bool isPublished);
    }
}