using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.DAL.Concrete.EntityFramework.Context
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

        public DbSet<Entity.Concrete.Customer> Customers { get; set; }
        public DbSet<Entity.Concrete.Address> Addresses { get; set; }
    }
}
