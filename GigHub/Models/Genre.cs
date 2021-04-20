using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Genre
    {
        public byte id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }
    }
}