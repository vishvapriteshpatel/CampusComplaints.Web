using CampusComplaints.Web.Data;
using CampusComplaints.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusComplaints.Web.Controllers.Api
{
    [ApiController]
    [Route("api/complaints")]
    public class ComplaintsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ComplaintsApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetAll()
        {
            var items = await _dbContext.Complaints
                .OrderByDescending(c => c.CreatedAtUtc)
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Complaint>> GetById(int id)
        {
            var item = await _dbContext.Complaints.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Complaint>> Create([FromBody] Complaint complaint)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            complaint.Id = 0;
            complaint.CreatedAtUtc = DateTime.UtcNow;
            _dbContext.Complaints.Add(complaint);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = complaint.Id }, complaint);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Complaint update)
        {
            var existing = await _dbContext.Complaints.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = update.Title;
            existing.Description = update.Description;
            existing.Category = update.Category;
            existing.ReporterName = update.ReporterName;
            existing.ReporterEmail = update.ReporterEmail;
            existing.Status = update.Status;
            existing.UpdatedAtUtc = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult> ChangeStatus(int id, [FromQuery] string status)
        {
            var existing = await _dbContext.Complaints.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Status = status;
            existing.UpdatedAtUtc = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _dbContext.Complaints.FindAsync(id);
            if (existing == null) return NotFound();
            _dbContext.Complaints.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}


