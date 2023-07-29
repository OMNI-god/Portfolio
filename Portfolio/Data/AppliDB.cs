using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Configuration;

namespace Portfolio.Data
{
    public class AppliDB:DbContext
    {
        public AppliDB(DbContextOptions<AppliDB> options):base(options)
        {
            
        }
        
        public DbSet<Investment> investments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Logs> logs { get; set; }
    }
}
