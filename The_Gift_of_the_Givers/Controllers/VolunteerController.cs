using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using The_Gift_of_the_Givers.Data;
using The_Gift_of_the_Givers.Models;
using The_Gift_of_the_Givers.ViewModels;

namespace The_Gift_of_the_Givers.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly GiftDbContext _context;

        public VolunteerController(GiftDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult VolunteerForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ApproveVolunteer(Guid userId)
        {
            try
            {
                // Your logic to approve the volunteer
                ViewData["SuccessMessage"] = "Volunteer approved successfully!";
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred while approving the volunteer: {ex.Message}";
            }

            return RedirectToAction("ManageVolunteers");
        }

        [HttpPost]
        public IActionResult DisapproveVolunteer(Guid userId)
        {
            try
            {
                // Your logic to disapprove the volunteer
                ViewData["SuccessMessage"] = "Volunteer disapproved successfully!";
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred while disapproving the volunteer: {ex.Message}";
            }

            return RedirectToAction("ManageVolunteers");
        }

        public IActionResult ManageVolunteers()
        {
            var volunteers = _context.Volunteers.ToList();
            return View(volunteers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitVolunteer(VolunteerViewModel volunteerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserID");

                if (!string.IsNullOrEmpty(userId))
                {
                    var volunteer = new Volunteer
                    {
                        VolunteerId = Guid.NewGuid(),
                        UserId = Guid.Parse(userId),
                        TaskType = volunteerViewModel.TaskType,
                        Availability = volunteerViewModel.Availability
                    };

                    _context.Volunteers.Add(volunteer);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Successmessage"); // Redirect to success message
                }
            }

            return View(volunteerViewModel); // Return the model if validation fails
        }

        public IActionResult Successmessage()
        {
            return View();
        }


}
}