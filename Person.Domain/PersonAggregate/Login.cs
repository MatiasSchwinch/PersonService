using System;
using System.Collections.Generic;

namespace Person.Domain.PersonAggregate
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string? Uuid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? Md5 { get; set; }
        public string? Sha1 { get; set; }
        public string? Sha256 { get; set; }

        public virtual PersonEntity? BasicDatum { get; set; }
    }
}
