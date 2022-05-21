using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class PictureDto
    {
        [Required]
        public int PictureId { get; set; }
        [MaxLength(60)]
        public string? Large { get; set; }
        [MaxLength(60)]
        public string? Medium { get; set; }
        [MaxLength(60)]
        public string? Thumbnail { get; set; }
    }
}