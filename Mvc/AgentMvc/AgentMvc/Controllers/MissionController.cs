using AgentMvc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentMvc.Controllers
{
    public class MissionController(IMissionSevice missionSevice) : Controller
    {
        
        public IActionResult Index()
        {
            var a = missionSevice.GetMissiosVM();
            return View(a);
        }

        
        public IActionResult Details(int id)
        {
            var a = missionSevice.GetMissiosVM().FirstOrDefault(m=> m.Id == id);
            return View(a);
        }

        
        
        public async Task<IActionResult> Assigned(int id)
        {
            var res = await missionSevice.AssignedMission(id);
            if (res != null)
            {
                return View("MissionComplate");
            }
            return View("MissionFaild");
        }

       
    }
}
