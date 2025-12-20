using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proj.Models;

namespace proj.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult homePage()
    {
        return View();
    }
}
