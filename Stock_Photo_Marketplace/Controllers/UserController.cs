using Microsoft.AspNetCore.Mvc;
using Stock_Photo_Marketplace.Models;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user) // Accepting the User model directly
    {
        Console.WriteLine($"Username: {user.Username}, Password: {user.PasswordHash}");

        if (user != null) // Check if the model state is valid
        {
            // Call the user service to attempt login
            var loggedInUser = _userService.Login(user.Username); 
            if(loggedInUser == null)
            {
                ViewBag.ErrorMessage = "No user found with this username.";
            }
            else if (loggedInUser.PasswordHash != user.PasswordHash)
            {
                ViewBag.ErrorMessage = "Incorrect password.";
            }
            else
            {
                // Set user session or cookie
                HttpContext.Session.SetString("User", loggedInUser.Username); // Save username in session
                return RedirectToAction("Index", "Home");
            }
        }
        
        return View();
    }


    [HttpPost]
    public IActionResult SignOut()
    {
        HttpContext.Session.Clear(); // Clear the session on sign out
        return RedirectToAction("Login", "User");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _userService.Register(user);
            return RedirectToAction("Login", "User");
        }

        return View(user); // Return to the register view if validation fails
    }
}
