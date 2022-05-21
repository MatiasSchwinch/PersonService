namespace Person.Domain.PersonAggregate
{
    public partial class Coordinate
    {
        public int CoordinatesId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual Location? Location { get; set; }
    }
}
