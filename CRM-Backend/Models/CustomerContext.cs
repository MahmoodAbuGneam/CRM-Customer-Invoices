using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PractiTech.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) 

        {    
        }

        public DbSet<Customer> Customers { get; set; } = null;

    }
}
