using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Contact
    {
        [ForeignKey("user")]
        public int id { get; set; }

        public int phone { get; set; }
        public string email { get; set; }

        public virtual User user { get; set; }

    }
}