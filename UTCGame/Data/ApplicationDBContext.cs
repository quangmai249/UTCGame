using Microsoft.EntityFrameworkCore;
using UTCGame.Areas.Employee.Models;
using UTCGame.Areas.FolderMedia.Models;

namespace UTCGame.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<FolderMediaModel> FolderMediaModel { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<UTCGame.Areas.Employee.Models.EmployeeModel> EmployeeModel { get; set; } = default!;
        //public DbSet<EmployeeModel> Employee { get; set; }
    }
}
