using Microsoft.EntityFrameworkCore;
using BTL_NhapMonCNPM.Models;

namespace BTL_NhapMonCNPM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
