using Person.Domain.SeedWork;

namespace Person.Domain.PersonAggregate
{
    public partial class PersonEntity : Entity, IAggregateRoot
    {
        public int PersonId { get; set; }
        public int? LocationId { get; set; }
        public int? LoginId { get; set; }
        public int? RegisteredId { get; set; }
        public int? PictureId { get; set; }
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

        public virtual Location? Location { get; set; }
        public virtual Login? Login { get; set; }
        public virtual Picture? Picture { get; set; }
        public virtual Registered? Registered { get; set; }
    }
}
