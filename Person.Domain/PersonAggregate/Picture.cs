using System;
using System.Collections.Generic;

namespace Person.Domain.PersonAggregate
{
    public partial class Picture
    {
        public int PictureId { get; set; }
        public string? Large { get; set; }
        public string? Medium { get; set; }
        public string? Thumbnail { get; set; }

        public virtual PersonEntity? BasicDatum { get; set; }
    }
}
