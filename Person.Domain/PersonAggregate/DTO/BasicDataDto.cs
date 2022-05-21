namespace Person.Domain.PersonAggregate.DTO
{
    public class BasicDataDto
    {
        public int PersonId { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public short Gender { get; set; }
        public DateOnly Date { get; set; }
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Cell { get; set; }
        public string? Nationality { get; set; }
    }
}
