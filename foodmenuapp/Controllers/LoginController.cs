using foodmenuapp.Models;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View(new LoginFormModel());
    }

    [HttpPost]
    public IActionResult Index(LoginFormModel model)
    {
        if (!ModelState.IsValid)
        {
            // If validation fails, return the view with validation errors
            return View(model);
        }

        // Process the login logic here
        // Redirect to the menu page if login is successful
        return RedirectToAction("Index", "Menu");
    }
}