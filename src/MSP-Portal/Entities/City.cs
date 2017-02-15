using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    /// <summary>
    /// Класс представления информации о городе.
    /// </summary>
    [Table("Cities")]
    public class City : BaseEntity
    {
        /// <summary>
        /// Название города.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор главы города/региона.
        /// </summary>
        public int LeadId { get; set; }
        
        /// <summary>
        /// Ссылка на профиль главы региона.
        /// </summary>
        public User Lead { get; set; }
        
        /// <summary>
        /// Список пользователей города.
        /// </summary>
        public IEnumerable<User> Users { get; set; }
    }
}
