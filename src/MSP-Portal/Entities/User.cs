using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Models.User.AccessRole Role { get; set; }
        public int City { get; set; }
        [ForeignKey("City")]
        public User Lead { get; set; }
        public DateTime Registred { get; set; }
        public DateTime LastVisit { get; set; }
        public bool Enable { get; set; }
    }
}
