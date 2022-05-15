namespace Person.Domain.PersonAggregate
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public int? CoordinatesId { get; set; }
        public int? TimezoneId { get; set; }
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Postcode { get; set; }

        public virtual Coordinate? Coordinates { get; set; }
        public virtual Timezone? Timezone { get; set; }
        public virtual PersonEntity? BasicDatum { get; set; }
    }
}
