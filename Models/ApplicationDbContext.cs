using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL_NMCNPM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaiHat> tbl_Baihat { get; set; }
    }

}
