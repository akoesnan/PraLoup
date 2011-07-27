using System.Web.Mvc;
using Ninject;
using PraLoup.Infrastructure.Data;

namespace PraLoup.WebApp.Utilities
{
    public class UnitOfWorkAttribute : FilterAttribute, IActionFilter
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public UnitOfWorkAttribute()
        {
            Order = 0;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UnitOfWork.Begin();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            UnitOfWork.End();
        }
    }
}
