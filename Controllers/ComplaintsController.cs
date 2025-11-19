using CampusComplaints.Web.Data;
using CampusComplaints.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusComplaints.Web.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ComplaintsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Complaints
        public async Task<IActionResult> Index()
        {
            var complaints = await _dbContext.Complaints
                .OrderByDescending(c => c.CreatedAtUtc)
                .ToListAsync();
            return View(complaints);
        }

        // GET: /Complaints/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var complaint = await _dbContext.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();
            return View(complaint);
        }

        // GET: /Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complaint complaint)
        {
            if (!ModelState.IsValid) return View(complaint);
            complaint.CreatedAtUtc = DateTime.UtcNow;
            _dbContext.Complaints.Add(complaint);
            await _dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Complaint submitted";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Complaints/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var complaint = await _dbContext.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();
            return View(complaint);
        }

        // POST: /Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Complaint complaint)
        {
            if (id != complaint.Id) return BadRequest();
            if (!ModelState.IsValid) return View(complaint);

            var existing = await _dbContext.Complaints.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Title = complaint.Title;
            existing.Description = complaint.Description;
            existing.Category = complaint.Category;
            existing.ReporterName = complaint.ReporterName;
            existing.ReporterEmail = complaint.ReporterEmail;
            existing.Status = complaint.Status;
            existing.UpdatedAtUtc = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Complaint updated";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Complaints/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await _dbContext.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();
            _dbContext.Complaints.Remove(complaint);
            await _dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Complaint deleted";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Complaints/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var complaint = await _dbContext.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();
            complaint.Status = status;
            complaint.UpdatedAtUtc = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}


