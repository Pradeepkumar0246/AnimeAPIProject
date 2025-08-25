using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimeAPIProject.Models
{
    public class Genre
    {
        [Key]
        public int Genre_Id { get; set; }
        public string Genre_Name { get; set; }
        public string Genre_Description { get; set; }

        [JsonIgnore]
        public ICollection<Anime> Animes { get; set; } = new List<Anime>();
    }
}
