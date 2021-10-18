using Domain.Entities.Companies;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}