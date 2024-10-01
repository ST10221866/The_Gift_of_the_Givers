using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using The_Gift_of_the_Givers.Data;
using The_Gift_of_the_Givers.Models;
using Microsoft.EntityFrameworkCore;
using The_Gift_of_the_Givers.ViewModels;

namespace The_Gift_of_the_Givers.Controllers
{
    public class IncidentReportsController : Controller
    {
        private readonly GiftDbContext _context;

        public IncidentReportsController(GiftDbContext context)
        {
            _context = context;
        }

        // GET: Display all incidents
        [HttpGet]
        public async Task<IActionResult> DisplayIncidents()
        {
            var incidents = await _context.IncidentReports.ToListAsync();

            // If no incidents are found, you can return a view with an empty list or handle as needed
            if (incidents == null || !incidents.Any())
            {
                return View(new List<IncidentReport>());
            }

            return View(incidents);
        }

        // GET: Show Incident Report form
        [HttpGet]
        public IActionResult ReportIncident()
        {
            return View();
        }

        // POST: Handle Incident Report submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportIncident(IncidentReportViewModel incidentReportViewModel)
        {
            if (ModelState.IsValid)
            {
                var incidentReport = new IncidentReport
                {
                    IncidentID = Guid.NewGuid(),
                    IncidentType = incidentReportViewModel.IncidentType,
                    Description = incidentReportViewModel.Description,
                    DateOfIncident = incidentReportViewModel.DateOfIncident,
                    ReporterName = incidentReportViewModel.ReporterName,
                    ReporterEmail = incidentReportViewModel.ReporterEmail,
                    Location = incidentReportViewModel.Location
                };

                _context.IncidentReports.Add(incidentReport);
                await _context.SaveChangesAsync();

                return RedirectToAction("IncidentConfirmation");
            }

            return View(incidentReportViewModel); // Return form with errors if validation fails
        }

        // GET: Incident confirmation page after successful submission
        public IActionResult IncidentConfirmation()
        {
            return View();
        }
    }
}
