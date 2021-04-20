using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GenreDto
    {
        public byte id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }
    }
}