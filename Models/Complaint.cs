using System.ComponentModel.DataAnnotations;

namespace CampusComplaints.Web.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(4000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = "General";

        [StringLength(100)]
        public string ReporterName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(256)]
        public string ReporterEmail { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAtUtc { get; set; }

        [StringLength(30)]
        public string Status { get; set; } = "Open"; // Open, InProgress, Resolved, Closed
    }
}


