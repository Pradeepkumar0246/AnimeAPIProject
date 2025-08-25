using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimeAPIProject.Models
{
    public class Users
    {
        [Key]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string Role { get; set; } // e.g., Admin, User, Guest

        public ICollection<Anime> WatchedAnimes { get; set; } = new List<Anime>();
    }
}
