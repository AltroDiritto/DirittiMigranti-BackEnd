using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirittoMigrantiAPI.Models;

namespace DirittoMigrantiAPI.Controllers
{
    public interface IPracticeController
    {
        Practice GetPublicPractice(long contentId);
        Practice GetPrivatePractice(long contentId);
        Practice NewPractice(Practice practice);
        bool SetPracticePrivacy(long contentId, bool newState);
        bool DeleteNews(long contentId);
    }
}
