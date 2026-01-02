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
    
    private string GetClientId()
    {
        if (!Request.Cookies.ContainsKey("ClientId"))
        {
            var id = Guid.NewGuid().ToString();
            Response.Cookies.Append("ClientId", id, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
            return id;
        }
        return Request.Cookies["ClientId"];
    }


    public IActionResult homePage()
    {
        var clientId = GetClientId();

        var model = new TodoViewModel
        {
            Tasks = _context.Tasks
                .Where(t => t.ClientId == clientId)
                .ToList()
        };

        return View(model);
    }


    [HttpPost]
    public IActionResult AddTask(TodoViewModel tvM)
    {
        var clientId = GetClientId();
        tvM.NewTask.ClientId = clientId;
        if (!ModelState.IsValid)
        {
            tvM.Tasks = _context.Tasks
                .Where(t => t.ClientId == clientId)
                .ToList();
            return View("homePage", tvM);
        }

        _context.Tasks.Add(tvM.NewTask);
        _context.SaveChanges();
        return RedirectToAction(nameof(homePage));
    }
    public IActionResult DeleteTask(int Id)
    {
        var clientId = GetClientId();
        var task = _context.Tasks.FirstOrDefault(t => t.Id == Id && t.ClientId == clientId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(homePage));
    }
    public IActionResult Complete(int Id)
    {
        var clientId = GetClientId();
        var task = _context.Tasks.FirstOrDefault(t => t.Id == Id && t.ClientId == clientId);
        if (task != null)
        {
            task.Completed = true;
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(homePage));
    }

    [HttpPost]
    public IActionResult EditTask(int Id, string Name, string Description)
    {
        var clientId = GetClientId();
        var task = _context.Tasks.FirstOrDefault(t => t.Id == Id && t.ClientId == clientId);
        if (task == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(Name))
        {
            task.Name = Name;
            task.Description = Description;
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(homePage));
    }
}
