using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class PersonEntityDto
    {
        [Required]
        public int PersonId { get; set; }
        [MaxLength(20)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;
        public short Gender { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public int Age { get; set; }
        [Required]
        [MaxLength(320)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string? Phone { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Cell { get; set; }
        [MaxLength(4)]
        public string? Nationality { get; set; }

        public LocationDto? Location { get; set; }
        public LoginDto? Login { get; set; }
        public PictureDto? Picture { get; set; }
        public RegisteredDto? Registered { get; set; }
    }
}
