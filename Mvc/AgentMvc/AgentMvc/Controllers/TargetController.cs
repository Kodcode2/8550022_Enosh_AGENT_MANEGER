using AgentMvc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentMvc.Controllers
{
    public class TargetController(ITargetService targetService) : Controller
    {
        // GET: TargetController
        public async Task<IActionResult> Index()
        {
            var t = targetService.GetTargets();
            return View(t);
        }

        // GET: TargetController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: TargetController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TargetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TargetController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: TargetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TargetController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: TargetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
