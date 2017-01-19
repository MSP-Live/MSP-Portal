using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP_Portal.Models
{
    public class User : Entities.User
    {
        public string Name { get; set; }
        public string Surname { get; set; }

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
