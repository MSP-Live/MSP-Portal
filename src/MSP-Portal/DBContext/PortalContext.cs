using Microsoft.EntityFrameworkCore;
using MSP_Portal.Entities;

namespace MSP_Portal.DBContext
{
    /// <summary>
    /// Класс структуры базы данных.
    /// </summary>
    public class PortalContext : DbContext
    {
        /// <summary>
        /// Таблица пользователей.
        /// </summary>
        public DbSet<User> UsersCollection { get; set; }
        
        /// <summary>
        /// Таблица городов.
        /// </summary>
        public DbSet<City> CitiesCollection { get; set; }

        /// <summary>
        /// Таблица активностей.
        /// </summary>
        public DbSet<Activity> ActivitiesCollection { get; set; }

        /// <summary>
        /// Таблица категорий активностей.
        /// </summary>
        public DbSet<ActivityType> ActivityTypesCollection { get; set; }

        /// <summary>
        /// Метод настройки связей таблиц.
        /// </summary>
        /// <param name="modelBuilder">Конструктор модели данных.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка внешних ключей.
            modelBuilder.Entity<Activity>()
                .HasOne(x => x.User)
                .WithMany(y => y.Activities)
                .HasForeignKey(z => z.UserId);

            modelBuilder.Entity<Activity>()
                .HasOne(x => x.ActivityType)
                .WithMany(y => y.Activities)
                .HasForeignKey(z => z.ActivityTypeId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.City)
                .WithMany(y => y.Users)
                .HasForeignKey(z => z.CityId);

            modelBuilder.Entity<City>()
                .HasOne(x => x.Lead)
                .WithOne(y => y.City)
                .HasForeignKey<City>(z => z.LeadId);
        }
    }
}
