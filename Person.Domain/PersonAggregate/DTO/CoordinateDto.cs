using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class CoordinateDto
    {
        [Required]
        public int CoordinatesId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}