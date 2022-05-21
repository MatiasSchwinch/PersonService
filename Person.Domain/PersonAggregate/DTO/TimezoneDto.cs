using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class TimezoneDto
    {
        [Required]
        public int TimezoneId { get; set; }
        [MaxLength(6)]
        public string? Offset { get; set; }
        [MaxLength(120)]
        public string? Description { get; set; }
    }
}