using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Helpers
{
    public class Login
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
