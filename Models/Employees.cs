using System.ComponentModel.DataAnnotations;

namespace dummyIdentity.Models
{
    public class Employees
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }

    }
}
