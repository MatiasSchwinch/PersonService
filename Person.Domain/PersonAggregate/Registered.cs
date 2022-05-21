namespace Person.Domain.PersonAggregate
{
    public partial class Registered
    {
        public int RegisteredId { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }

        public virtual PersonEntity? BasicDatum { get; set; }
    }
}
