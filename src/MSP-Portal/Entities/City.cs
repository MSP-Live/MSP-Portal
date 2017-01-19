using System.ComponentModel.DataAnnotations.Schema;

namespace MSP_Portal.Entities
{
    [Table("Cities")]
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int? Lead { get; set; }
    }
}
