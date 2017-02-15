using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    /// <summary>
    /// Класс представления активности.
    /// </summary>
    [Table("Activities")]
    public class Activity : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Ссылка на профиль пользователя.
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// Название активности.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор типа активности.
        /// </summary>
        public int ActivityTypeId { get; set; }

        /// <summary>
        /// Ссылка на тип активности.
        /// </summary>
        [ForeignKey("ActivityTypeId")]
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// Примичания активности.
        /// </summary>
        public string Note { get; set; }
    }
}
