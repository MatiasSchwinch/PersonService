using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class RegisteredDto
    {
        [Required]
        public int RegisteredId { get; set; }
        public DateOnly Date { get; set; }
        public int Age { get; set; }
    }
}