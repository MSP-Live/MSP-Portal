using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    [Table("Activities")]
    public class Activity : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int ActivityTypeId { get; set; }
        [ForeignKey("ActivityTypeId")]
        public ActivityType ActivityType { get; set; }
        public string Note { get; set; }
    }
}
