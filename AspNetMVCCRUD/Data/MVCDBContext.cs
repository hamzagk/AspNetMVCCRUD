using AspNetMVCCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVCCRUD.Data
{
    public class MVCDBContext : DbContext
    {
        // ShortCut ctrl+. generate constructor
        public MVCDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
