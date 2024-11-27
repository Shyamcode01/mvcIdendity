using System.ComponentModel.DataAnnotations;

namespace dummyIdentity.Models
{
    public class Contact
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }

        public string TextMessage { get; set; }
    }
}
