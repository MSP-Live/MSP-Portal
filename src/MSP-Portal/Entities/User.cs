using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    /// <summary>
    /// Класс представления информации о пользователе.
    /// </summary>
    [Table("Users")]
    public class User : BaseEntity
    {
        /// <summary>
        /// Список каналов соединений.
        /// </summary>
        public IEnumerable<string> ChatIds { get; set; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }
        
        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public Models.User.AccessRole Role { get; set; }
        
        /// <summary>
        /// Идентификатор города.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Ссылка на город проживания.
        /// </summary>
        [ForeignKey("CityId")]
        public City City { get; set; }
        
        /// <summary>
        /// Информация о главе региона.
        /// </summary>
        public virtual User Lead { get { return City.Lead; } }

        /// <summary>
        /// Время регистрации пользователя.
        /// </summary>
        public DateTime Registred { get; set; }

        /// <summary>
        /// Время последней активности пользователя.
        /// </summary>
        public DateTime LastVisit { get; set; }

        /// <summary>
        /// Доступность пользователя.
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Список активностей пользователя.
        /// </summary>
        public IEnumerable<Activity> Activities { get; set; }
    }
}
