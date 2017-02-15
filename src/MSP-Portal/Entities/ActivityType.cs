using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    /// <summary>
    /// Класс представления типов активностей.
    /// </summary>
    [Table("ActivitiesTypes")]
    public class ActivityType : BaseEntity
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ценность активности в очках.
        /// </summary>
        public int Scores { get; set; }

        /// <summary>
        /// Список активностей типа.
        /// </summary>
        public IEnumerable<Activity> Activities { get; set; }
    }
}
