using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class LocationDto
    {
        [Required]
        public int LocationId { get; set; }
        public int StreetNumber { get; set; }
        [MaxLength(90)]
        public string? StreetName { get; set; }
        [MaxLength(60)]
        public string? City { get; set; }
        [MaxLength(60)]
        public string? State { get; set; }
        [MaxLength(60)]
        public string? Country { get; set; }
        [MaxLength(30)]
        public string? Postcode { get; set; }

        public CoordinateDto? Coordinates { get; set; }
        public TimezoneDto? Timezone { get; set; }
    }
}