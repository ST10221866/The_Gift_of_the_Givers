using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using The_Gift_of_the_Givers.Data;
using The_Gift_of_the_Givers.Models;
using The_Gift_of_the_Givers.ViewModels;
using Microsoft.AspNetCore.Http;

namespace The_Gift_of_the_Givers.Controllers
{
    public class DonationsController : Controller
    {
        private readonly GiftDbContext _context;

        public DonationsController(GiftDbContext context)
        {
            _context = context;
        }

        // GET: Show donation form
        [HttpGet]
        public IActionResult Donate()
        {
            return View();
        }
        
        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Donate(DonationViewModel donationViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserID");

                if (!string.IsNullOrEmpty(userId))
                {
                    var donation = new Donation
                    {
                        DonationID = Guid.NewGuid(),
                        UserID = Guid.Parse(userId),
                        DonationType = donationViewModel.DonationType,
                        Quantity = donationViewModel.Quantity,
                        Description = donationViewModel.Description,
                        DonationDate = DateTime.Now,
                        IsDistributed = false,
                        DonorName = donationViewModel.DonorName,  
                        DonorEmail = donationViewModel.DonorEmail, 
                        DeliveryDate = donationViewModel.DeliveryDate 
                    };

                    _context.donations.Add(donation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Success");
                }
            }

            return View(donationViewModel); // Return the model if validation fails
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _context.donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Delete Donation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var donation = await _context.donations.FindAsync(id);
            if (donation != null)
            {
                _context.donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Managedonations");
        }

        public async Task<IActionResult> Managedonations()
        {
            var donations = await _context.donations.Include(d => d.User).ToListAsync();
            return View(donations);
        }

        // GET: List of donations
        public async Task<IActionResult> Index()
        {
            var donations = await _context.donations.Include(d => d.User).ToListAsync();
            return View(donations);
        }

        // GET: Manage distribution status
        public async Task<IActionResult> Distribute(Guid id)
        {
            var donation = await _context.donations.FindAsync(id);
            if (donation != null)
            {
                donation.IsDistributed = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
