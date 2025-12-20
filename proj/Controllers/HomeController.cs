using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proj.Models;

namespace proj.Controllers;

public class HomeController : Controller
{
    private readonly DBTodolist _context;

    public HomeController(DBTodolist context)
    {
        _context = context;
    }

    public IActionResult homePage()
    {
        var tvmodel = new TodoViewModel
        {
            Tasks = _context.Tasks.ToList()
        };
        return View(tvmodel);
    }
            [HttpPost]
        public IActionResult AddTask(TodoViewModel tvM)
        {
            if (!ModelState.IsValid)
            {
                tvM.Tasks = _context.Tasks.ToList();
                return View("HomePage", tvM);
            }

            _context.Tasks.Add(tvM.NewTask);
            _context.SaveChanges();
            return RedirectToAction(nameof(homePage));
        }
}
