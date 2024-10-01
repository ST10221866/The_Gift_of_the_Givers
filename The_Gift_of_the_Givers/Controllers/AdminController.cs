using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using The_Gift_of_the_Givers.Data;
using The_Gift_of_the_Givers.Models;
using The_Gift_of_the_Givers.ViewModels;

namespace The_Gift_of_the_Givers.Controllers
{
    public class AdminController : Controller
    {
        private readonly GiftDbContext _context;

        public AdminController(GiftDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins.SingleOrDefaultAsync(a => a.Username == model.Username);
                if (admin != null && VerifyPassword(model.Password, admin.PasswordHash))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        // GET: Admin/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (await _context.Admins.AnyAsync(a => a.Username == model.Username))
                {
                    ModelState.AddModelError(string.Empty, "Username already exists.");
                    return View(model);
                }

                // Create a new admin and hash the password
                var newAdmin = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password) // Use your hashing method
                };

                _context.Admins.Add(newAdmin);
                await _context.SaveChangesAsync();

                // Optionally log in the admin after registration
                // await HttpContext.SignInAsync(...);

                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        // Password hashing method
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Password verification method
        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
