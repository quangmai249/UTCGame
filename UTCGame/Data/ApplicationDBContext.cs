using Microsoft.EntityFrameworkCore;
using UTCGame.Areas.Employee.Models;
using UTCGame.Areas.FolderMedia.Models;
using UTCGame.Areas.Game.Models;
using UTCGame.Areas.Product.Models;

namespace UTCGame.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<FolderMediaModel> FolderMediaModel { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<EmployeeModel> EmployeeModel { get; set; }
        public DbSet<GameType> GameType { get; set; }
        public DbSet<GamePlatform> GamePlatform { get; set; }
        public DbSet<GameModel> Game { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
    }
}
