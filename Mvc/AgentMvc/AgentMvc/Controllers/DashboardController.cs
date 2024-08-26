using AgentMvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentMvc.Controllers
{
    public class DashboardController(IGeneralDashboardService dashboardService) : Controller
    {
        public  IActionResult Index()
        {
            var desh = dashboardService.GetGeneralDashboard();
            var a = 5;
            return View(desh);
        }
    }
}
