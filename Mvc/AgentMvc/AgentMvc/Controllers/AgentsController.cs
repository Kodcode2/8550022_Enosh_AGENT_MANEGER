using AgentMvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentMvc.Controllers
{
    public class AgentsController(IAgentService agentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var agents = await agentService.GetAgents();
            return View(agents);
        }
    }
}
