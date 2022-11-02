using Hotel.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<UserLogin> Users { get; set; }
        
    }
}
