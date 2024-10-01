using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using The_Gift_of_the_Givers.Models;
using The_Gift_of_the_Givers.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using The_Gift_of_the_Givers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace The_Gift_of_the_Givers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GiftDbContext _context;

        public HomeController(ILogger<HomeController> logger, GiftDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult UpdateProfile()
        //{
        //    return View();
        //}




        // GET: Display the user profile for editing
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var userId = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user ID is not found
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == Guid.Parse(userId));

            if (user == null)
            {
                return NotFound(); // Handle case where the user doesn't exist
            }

            var viewModel = new UpdateProfileViewModel
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNumber = user.ContactNumber,
                Address = user.Address,
                City = user.City,
                State = user.State
            };

            return View(viewModel);
        }

        // POST: Update the user profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.UserID);

                if (user == null)
                {
                    return NotFound(); // Handle case where the user doesn't exist
                }

                // Update user properties
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.ContactNumber = model.ContactNumber;
                user.Address = model.Address;
                user.City = model.City;
                user.State = model.State;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("ProfileUpdated"); // Redirect to a success page or profile view
            }

            return View(model); // Return the model if validation fails
        }

        // Method to display a success message after profile update
        public IActionResult ProfileUpdated()
        {
            return View();
        }









        // Corrected Profile method to pass the model to the view
        public async Task<IActionResult> Profile()
        {
            // Retrieve the current user's ID from the session
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
            {
                // If no user is logged in, redirect to login page
                return RedirectToAction("Login");
            }

            // Convert the string user ID to GUID
            var userId = Guid.Parse(userIdString);

            // Retrieve the user from the database using the session user ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                // If the user is not found, return unauthorized
                return Unauthorized();
            }

            // Initialize the ProfileViewModel with user data
            var model = new ProfileViewModel
            {
                Username = user.Username,
                Email = user.Email
            };

            // Pass the model to the view
            return View(model);
        }

        // GET: Show registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Handle registration form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists in the database
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already exists. Please choose a different username.");
                    return View(model);
                }

                // Check if the email already exists in the database
                var existingEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingEmail != null)
                {
                    // User with the same email already exists
                    ModelState.AddModelError("", "Email is already registered. Please use a different email.");
                    return View(model);
                }

                // Hash the password
                var passwordHasher = new PasswordHasher<Users>();
                var hashedPassword = passwordHasher.HashPassword(null, model.Password);

                // Create a new user object
                var newUser = new Users
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(); // Save user to the database

                // Log successful registration
                _logger.LogInformation("New user registered: " + newUser.Username);

                return RedirectToAction("Profile"); // Redirect to profile after successful registration
            }

            // Return view with validation errors
            return View(model);
        }

        // GET: Show login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.IsActive);

                // Check if the user exists
                if (user != null)
                {
                    // Log information about the found user
                    _logger.LogInformation("User found: " + user.Username);

                    // Check for null or empty values for critical fields like PasswordHash and Username
                    if (!string.IsNullOrEmpty(user.PasswordHash) && !string.IsNullOrEmpty(user.Username))
                    {
                        // Verify the hashed password
                        var passwordHasher = new PasswordHasher<Users>();
                        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                        if (result == PasswordVerificationResult.Success)
                        {
                            HttpContext.Session.SetString("UserID", user.UserID.ToString());

                            // Redirect to the homepage or user dashboard
                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            // Password doesn't match, return an error
                            ModelState.AddModelError("", "Invalid username or password.");
                        }
                    }
                    else
                    {
                        // One of the required fields is null or empty
                        ModelState.AddModelError("", "User data is incomplete. Please contact support.");
                    }
                }
                else
                {
                    // No user found with the provided username
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, re-display the form
            return View(model);
        }

        // GET: Manage user profile (Restrict access to the user's own profile only)
        [Authorize] // Ensures only authenticated users can access this action
        public async Task<IActionResult> ManageProfile()
        {
            // Retrieve the current user's ID from the session
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login");
            }

            // Convert string to GUID
            var userId = Guid.Parse(userIdString);

            // Retrieve the user from the database using the session ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                return Unauthorized(); // User not found, unauthorized access
            }

            // Map the user information to the ViewModel for display in the view
            var model = new ProfileViewModel
            {
                Username = user.Username,
                Email = user.Email
            };

            return View(model);
        }



        // Log out the user
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Login"); // Redirect to login after logout
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}