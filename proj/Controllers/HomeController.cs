using System.Diagnostics;
using System.Net.NetworkInformation;
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
            return View("homePage", tvM);
        }

        _context.Tasks.Add(tvM.NewTask);
        _context.SaveChanges();
        return RedirectToAction(nameof(homePage));
    }
    public IActionResult DeleteTask(int Id)
    {
        var task = _context.Tasks.Find(Id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(homePage));
    }
    public IActionResult Complete(int Id)
    {
        var task = _context.Tasks.Find(Id);
        task.Completed = true;
        _context.SaveChanges();
        return RedirectToAction(nameof(homePage));
    }
}
