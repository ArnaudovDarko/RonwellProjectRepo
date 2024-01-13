using Microsoft.EntityFrameworkCore;
using RonwellProject.Models;

namespace RonwellProject.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<EmployeeInfo>? Employees { get; set; }
    }
}
