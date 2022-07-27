using Microsoft.EntityFrameworkCore;
using PortalBackend.Objects.User;

namespace PortalBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserDTO> Users { get; set; }

        public DbSet<Communities> Communities { get; set; }

        public DbSet<Subscriptions> Subscriments { get; set; }


    }

}



