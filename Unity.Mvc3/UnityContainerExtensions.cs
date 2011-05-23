using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Praloup.UnityExtension
{
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Registers all controllers in the calling assembly
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterControllers(this UnityContainer container)
        {
            var controllerTypes = (from t in Assembly.GetCallingAssembly().GetTypes()
                                   where typeof(IController).IsAssignableFrom(t) && !t.IsAbstract
                                   select t).ToList();

            controllerTypes.ForEach(t => container.RegisterType(t));
        }
    }
}