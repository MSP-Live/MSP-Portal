using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    [Table("ActivitiesTypes")]
    public class ActivityType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Scores { get; set; }
    }
}
