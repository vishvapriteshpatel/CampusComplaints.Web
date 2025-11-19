using Microsoft.EntityFrameworkCore;
using CampusComplaints.Web.Models;

namespace CampusComplaints.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Complaint> Complaints => Set<Complaint>();
    }
}


