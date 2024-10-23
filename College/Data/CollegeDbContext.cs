using College.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}
