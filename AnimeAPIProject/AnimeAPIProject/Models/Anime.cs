using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AnimeAPIProject.Models
{
    public class Anime
    {
        [Key]
        public int Anime_Id { get; set; }
        public string Anime_Name { get; set; }
        public int Anime_Episodes { get; set; }
        public DateTime Anime_Release_Date { get; set; }
        public string Anime_Description { get; set; }
        public int Anime_Seasons { get; set; }

        // Foreign Key
        public int Studio_Id { get; set; }
        public Studio? Studio { get; set; }

        // Relationships
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

       // [JsonIgnore] // prevent loop
        public ICollection<Users> Users { get; set; } = new List<Users>();
        [NotMapped]
        [JsonIgnore]
        public List<int>? GenreIds { get; set; }
    }
}
