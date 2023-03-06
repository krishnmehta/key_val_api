using System.ComponentModel.DataAnnotations;

namespace Key_value_service.Models
{
    public class KeyValue
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
