using Microsoft.EntityFrameworkCore;
using MSP_Portal.Entities;

namespace MSP_Portal.DBContext
{
    public class PortalContext : DbContext
    {
        public DbSet<User> UsersCollection { get; set; }
        public DbSet<City> CitiesCollection { get; set; }
        public DbSet<Activity> ActivitiesCollection { get; set; }
        public DbSet<ActivityType> ActivityTypesCollection { get; set; }
    }
}
