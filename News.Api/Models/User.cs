using System.ComponentModel.DataAnnotations;

namespace News.Api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }
    }
}
