using Microsoft.EntityFrameworkCore;

namespace CQRSWebApiProject.DAL.Concrete.EntityFramework.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entity.Concrete.Customer> Customers { get; set; }

        public DbSet<Entity.Concrete.Address> Addresses { get; set; }
    }
}
