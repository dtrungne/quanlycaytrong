using Microsoft.EntityFrameworkCore;
using CayTrongMVC.Models;  // Đảm bảo có namespace này

namespace CayTrongMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CayTrong> CayTrong { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
    }
}
