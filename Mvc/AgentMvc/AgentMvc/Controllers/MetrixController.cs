using Microsoft.AspNetCore.Mvc;
using AgentMvc.Service;

namespace AgentMvc.Controllers
{
    public class MetrixController(IDataStore dataStore) : Controller
    {
        public IActionResult Index()
        {
            var matrix = dataStore.Metrix(dataStore.AllTarget, dataStore.AllAgents);
            return View(matrix);
        }
    }
}
