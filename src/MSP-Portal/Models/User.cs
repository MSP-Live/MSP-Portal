namespace MSP_Portal.Models
{
    /// <summary>
    /// Класс модели представления информации о пользователе.
    /// </summary>
    public class User : Entities.User
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public enum AccessRole
        {
            God,
            Head,
            Admin,
            Lead,
            RegionLead,
            User,
            None
        }
    }
}
