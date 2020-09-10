namespace DataAccessLayer.Models
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public virtual Contact contact { get; set; }
    }
}