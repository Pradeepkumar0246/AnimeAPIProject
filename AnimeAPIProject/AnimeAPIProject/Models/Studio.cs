using System.ComponentModel.DataAnnotations;

namespace AnimeAPIProject.Models
{
    public class Studio
    {
        [Key]
        public int Studio_Id { get; set; }
        public string Studio_Name { get; set; }
        public int Studio_Year { get; set; }
        public string Studio_Description { get; set; }
        public ICollection<Anime> Animes { get; set; } = new List<Anime>();
    }
}
