namespace Person.Domain.PersonAggregate
{
    public partial class Timezone
    {
        public int TimezoneId { get; set; }
        public string? Offset { get; set; }
        public string? Description { get; set; }
        public virtual Location? Location { get; set; }
    }
}
