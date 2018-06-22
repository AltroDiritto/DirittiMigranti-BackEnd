using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface INewsController
    {
        News GetNews(long id);

        News NewNews();
    }
}